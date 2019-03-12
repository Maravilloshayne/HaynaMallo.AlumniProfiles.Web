using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hayna.AlumniProfileEntries.Web.Areas.ViewModels.Alumni;
using Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Helpers;
using Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Hayna.AlumniProfileEntries.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AlumniProfileEntriesController : Controller
    {
        private readonly DefaultDbContext _context;
        private IHostingEnvironment _env;

        public AlumniProfileEntriesController(DefaultDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }



        [HttpGet, Route("manage/alumniprofiles/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("manage/alumniprofiles/create")]
        public IActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            AlumniProfile alumniprofile = new AlumniProfile()
            {
                Id = Guid.NewGuid(),
                Position = model.Position,
                Company = model.Company,
                Location = model.Location,
                Description = model.Description,
                FromDate = model.FromDate,
                ToDate = model.ToDate
            };

            this._context.AlumniProfiles.Add(alumniprofile);
            this._context.SaveChanges();

            return View();
        }


        [HttpGet, Route("manage/alumniprofiles/index")]
        [HttpGet, Route("manage/alumniprofiles")]
        public IActionResult Index(int pageIndex = 1, int pageSize = 5, string keyword = "")
        {
            Page<AlumniProfile> result = new Page<AlumniProfile>();

            if (pageSize < 1)
            {
                pageSize = 1;
            }

            IQueryable<AlumniProfile> alumniprofQuery = (IQueryable<AlumniProfile>)this._context.AlumniProfiles;

            if (string.IsNullOrEmpty(keyword) == false)
            {
                alumniprofQuery = alumniprofQuery.Where(u => u.Company.ToLower().Contains(keyword.ToLower()));
            }

            long queryCount = alumniprofQuery.Count();

            int pageCount = (int)Math.Ceiling((decimal)(queryCount / pageSize));
            long mod = (queryCount % pageSize);

            if (mod > 0)
            {
                pageCount = pageCount + 1;
            }

            int skip = (int)(pageSize * (pageIndex - 1));
            List<AlumniProfile> users = alumniprofQuery.ToList();

            result.Items = users.Skip(skip).Take((int)pageSize).ToList();
            result.PageCount = pageCount;
            result.PageSize = pageSize;
            result.QueryCount = queryCount;
            result.PageIndex = pageIndex;
            result.Keyword = keyword;

            return View(new IndexViewModel()
            {
                AlumniProfiles = result
            });
        }

        [HttpPost, Route("manage/alumniprofiles/unpublish")]
        public IActionResult Unpublish(AlumniProfileIdViewModel model)
        {
            var alumniprofile = this._context.AlumniProfiles.FirstOrDefault(p => p.Id == model.Id);
            if (alumniprofile != null)
            {
                alumniprofile.IsPublished = false;
                this._context.AlumniProfiles.Update(alumniprofile);
                this._context.SaveChanges();
                return Ok();
            }
            return null;
        }

        [HttpPost, Route("manage/alumniprofiles/publish")]
        public IActionResult Publish(AlumniProfileIdViewModel model)
        {
            var alumniprofile = this._context.AlumniProfiles.FirstOrDefault(a => a.Id == model.Id);
            if (alumniprofile != null)
            {
                alumniprofile.IsPublished = true;
                this._context.AlumniProfiles.Update(alumniprofile);
                this._context.SaveChanges();
                return Ok();
            }
            return null;
        }

        [HttpGet, Route("/manage/alumniprofiles/update-company/{alumniprofileId}")]
        public IActionResult UpdateCompany(Guid? alumniprofileId)
        {
            var alumniprofile = this._context.AlumniProfiles.FirstOrDefault(a => a.Id == alumniprofileId);
            if (alumniprofile != null)
            {
                var model = new UpdateCompanyViewModel()
                {
                    AlumniProfileId = alumniprofile.Id,
                    Description = alumniprofile.Description,
                    Company = alumniprofile.Company,
                    Position = alumniprofile.Position,
                    Location = alumniprofile.Location,
                    FromDate = alumniprofile.FromDate,
                    ToDate = alumniprofile.ToDate
                };
                return View(model);
            }
            return RedirectToAction("Create");
        }
        [HttpPost, Route("/manage/alumniprofiles/update-company")]
        public IActionResult UpdateCompany(UpdateCompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var alumniprofile = this._context.AlumniProfiles.FirstOrDefault(a => a.Id == model.AlumniProfileId);
            if (alumniprofile != null)
            {
                alumniprofile.Company = model.Company;
                alumniprofile.Description = model.Description;
                alumniprofile.FromDate = model.FromDate;
                alumniprofile.ToDate = model.ToDate;
                alumniprofile.Position = model.Position;
                alumniprofile.Location = model.Location;
                this._context.AlumniProfiles.Update(alumniprofile);
                this._context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet, Route("/manage/alumniprofiles/update-thumbnail/{alumniprofileId}")]
        public IActionResult Thumbnail(Guid? alumniprofileId)
        {
            return View(new ThumbnailViewModel() { AlumniProfileId = alumniprofileId });
        }
        [HttpPost, Route("/manage/alumniprofiles/update-thumbnail")]
        public async Task<IActionResult> Thumbnail(ThumbnailViewModel model)
        {
            //Check file size of the uploaded thumbnail
            //reject if the file is greater than 2mb
            var fileSize = model.Thumbnail.Length;
            if ((fileSize / 1048576.0) > 2)
            {
                ModelState.AddModelError("", "The file you uploaded is too large. Filesize limit is 2mb.");
                return View(model);
            }
            //Check file type of the uploaded thumbnail
            //reject if the file is not a jpeg or png
            if (model.Thumbnail.ContentType != "image/jpeg" && model.Thumbnail.ContentType != "image/png")
            {
                ModelState.AddModelError("", "Please upload a jpeg or png file for the thumbnail.");
                return View(model);
            }
            //Formulate the directory where the file will be saved
            //create the directory if it does not exist
            var dirPath = _env.WebRootPath + "/alumniprofiles/" + model.AlumniProfileId.ToString();
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            //always name the file thumbnail.png
            var filePath = dirPath + "/thumbnail.png";
            if (model.Thumbnail.Length > 0)
            {
                //Open a file stream to read all the file data into a byte array
                byte[] bytes = await FileBytes(model.Thumbnail.OpenReadStream());
                //load the file into the third party (ImageSharp) Nuget Plugin                
                using (Image<Rgba32> image = Image.Load(bytes))
                {
                    //use the Mutate method to resize the image 150px wide by 150px long
                    image.Mutate(x => x.Resize(150, 150));
                    //save the image into the path formulated earlier
                    image.Save(filePath);
                }
            }
            return RedirectToAction("Thumbnail", new { AlumniProfileId = model.AlumniProfileId });

        }


        //this method is used to load the file stream into 
        //a byte array
        public async Task<byte[]> FileBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        [HttpGet, Route("/manage/alumniprofiles/update-description/{alumniprofileId}")]
        public IActionResult UpdateDescription(Guid? alumniprofileId)
        {
            var alumniprofile = this._context.AlumniProfiles.FirstOrDefault(p => p.Id == alumniprofileId);
            if (alumniprofile != null)
            {
                return View(new UpdateDescriptionViewModel()
                {
                    AlumniProfileId = alumniprofile.Id,
                    Company = alumniprofile.Company,
                    Position = alumniprofile.Position,
                    Location = alumniprofile.Location,
                    FromDate = alumniprofile.FromDate,
                    ToDate = alumniprofile.ToDate,
                    Description = alumniprofile.Description
                });
            }
            return RedirectToAction("Index");
        }
        [HttpPost, Route("/manage/alumniprofiles/update-description/")]
        public IActionResult UpdateDescription(UpdateDescriptionViewModel model)
        {
            var alumniprofile = this._context.AlumniProfiles.FirstOrDefault(p => p.Id == model.AlumniProfileId);
            if (alumniprofile != null)
            {
                alumniprofile.Company = model.Company;
                alumniprofile.Position = model.Position;
                alumniprofile.Location = model.Location;
                alumniprofile.FromDate = model.FromDate;
                alumniprofile.ToDate = model.ToDate;
                alumniprofile.Description = model.Description;
                alumniprofile.Timestamp = DateTime.UtcNow;
                this._context.AlumniProfiles.Update(alumniprofile);
                this._context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet, Route("/manage/alumniprofiles/update-info/{alumniprofileId}")]
        public IActionResult UpdateInfoViewModel(Guid? alumniprofileId)
        {
            var alumniprofile = this._context.AlumniProfiles.FirstOrDefault(a => a.Id == alumniprofileId);
            if (alumniprofile != null)
            {
                return View(new UpdateDescriptionViewModel()
                {
                    AlumniProfileId = alumniprofile.Id,
                    Company = alumniprofile.Company,
                    Position = alumniprofile.Position,
                    Location = alumniprofile.Location,
                    FromDate = alumniprofile.FromDate,
                    ToDate = alumniprofile.ToDate,
                    Description = alumniprofile.Description
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost, Route("/manage/alumniprofiles/update-info/")]
        public IActionResult UpdateInfoViewModel(UpdateDescriptionViewModel model)
        {
            var alumniprofile = this._context.AlumniProfiles.FirstOrDefault(a => a.Id == model.AlumniProfileId);
            if (alumniprofile != null)
            {
                alumniprofile.Company = model.Company;
                alumniprofile.Position = model.Position;
                alumniprofile.Location = model.Location;
                alumniprofile.FromDate = model.FromDate;
                alumniprofile.ToDate = model.ToDate;
                alumniprofile.Description = model.Description;
                alumniprofile.Timestamp = DateTime.UtcNow;
                this._context.AlumniProfiles.Update(alumniprofile);
                this._context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
