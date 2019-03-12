using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hayna.AlumniProfileEntries.Web.Models;
using Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Helpers;
using Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Hayna.AlumniProfileEntries.Web.VIewModels.AlumniProfiles;

namespace Hayna.AlumniProfileEntries.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DefaultDbContext _context;
        private IHostingEnvironment _env;

        public HomeController(DefaultDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int pageIndex = 1, int pageSize = 10, string keyword = "")
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

        public IActionResult Init()
        {
            var alumniprofile = this._context.AlumniProfiles.FirstOrDefault();

            if (alumniprofile == null)
            {
                AlumniProfile post1 = new AlumniProfile()
                {
                    Id = Guid.Parse("3a88bea9-8a65-4c23-941a-972a6195b940"),
                    Description = @"The International 2018 was the eight iteration of 
                                Dota 2's flagship annual championship. Hosted by Valve Corporation,
                                it took place at the Rogers Arena in Vancouver,
                                British Columbia, being the first to be celebrated 
                                outside of the United States.",
                  IsPublished = true,
                    Position = "Worker",
                    ToDate = DateTime.UtcNow,
                    Location = "CSM BATAAN",
                    Timestamp = DateTime.UtcNow,
                    Company= "Lakad Matatag!!!"
                };
                this._context.AlumniProfiles.Add(post1);


                AlumniProfile post2 = new AlumniProfile()
                {
                    Id = Guid.Parse("3a88bea9-8a65-4c23-941a-972a6195b940"),
                    Description = @"The International 2018 was the eight iteration of 
                                Dota 2's flagship annual championship. Hosted by Valve Corporation,
                                it took place at the Rogers Arena in Vancouver,
                                British Columbia, being the first to be celebrated 
                                outside of the United States.",
                    IsPublished = true,
                    Position = "Worker2",
                    ToDate = DateTime.UtcNow,
                    Location = "CSM BATAAN",
                    Timestamp = DateTime.UtcNow,
                    Company = "Exbatallion"
                };
                this._context.AlumniProfiles.Add(post2);

                AlumniProfile post3 = new AlumniProfile()
                {
                    Id = Guid.Parse("3a88bea9-8a65-4c23-941a-972a6195b940"),
                    Description = @"The International 2018 was the eight iteration of 
                                Dota 2's flagship annual championship. Hosted by Valve Corporation,
                                it took place at the Rogers Arena in Vancouver,
                                British Columbia, being the first to be celebrated 
                                outside of the United States.",
                    IsPublished = true,
                    Position = "Worker3",
                    ToDate = DateTime.UtcNow,
                    Location = "CSM BATAAN",
                    Timestamp = DateTime.UtcNow,
                    Company = "HypeBeast!!!"
                };
                this._context.AlumniProfiles.Add(post3);

                this._context.SaveChanges();
            }

            var user = this._context.Users.FirstOrDefault();
            if (user == null)
            {
                var admin = new User()
                {
                    Id = Guid.Parse("b2e5a4fc-ca4e-4d3f-b9ac-d8a088cd6401"),
                    EmailAddress = "HaynaMallo@gmail.com",
                    FirstName = "Hayna",
                    LastName = "Mallo",
                    Gender = Infrastructures.Data.Enums.Gender.Male,
                    LoginStatus = Infrastructures.Data.Enums.LoginStatus.Active,
                    LoginTrials = 0,
                    RegistrationCode = RandomString(6),
                    Password = BCrypt.BCryptHelper.HashPassword("Accord605", BCrypt.BCryptHelper.GenerateSalt(8))
                };
                this._context.Users.Add(admin);
                this._context.SaveChanges();
                this._context.UserRoles.Add(new UserRole()
                {
                    Id = Guid.Parse("b2e5a4fc-ca4e-4d3f-b9ac-d8a088cd6401"),
                    Role = Infrastructures.Data.Enums.Role.Admin,
                    UserId = admin.Id
                });
                this._context.SaveChanges();
            }
            return RedirectToAction("index");
            //return RedirectPermanent("~/posts/index");
        }
        private Random random = new Random();
        private string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
