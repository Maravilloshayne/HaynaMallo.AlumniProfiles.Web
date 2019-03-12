using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeKicker.BBCode;
using Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Helpers;
using Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Models;
using Hayna.AlumniProfileEntries.Web.VIewModels.AlumniProfiles;
using Microsoft.AspNetCore.Mvc;

namespace Hayna.AlumniProfileEntries.Web.Controllers
{
    public class AlumniProfileEntriesController : Controller
    {
        private readonly DefaultDbContext _context;

        public AlumniProfileEntriesController(DefaultDbContext context)
        {
            _context = context;
        }

        [HttpGet, Route("alumniprofiles")]
        [HttpGet, Route("alumniprofiles/index")]
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

        [HttpGet, Route("alumniprofiles/detail/{alumniprofilesId}")]
        public IActionResult AlumniProfile(Guid? alumniprofilesId)
        {
            var alumniprofile = this._context.AlumniProfiles.FirstOrDefault(p => p.Id == alumniprofilesId);

            if (alumniprofile != null)
            {
                return View(new AlumniProfileViewModel()
                {
                    AlumniProfileId = alumniprofile.Id,
                    Company = alumniprofile.Company,
                    Position = alumniprofile.Position,
                    Location = alumniprofile.Location,
                    Description = alumniprofile.Description,
                    FromDate = alumniprofile.FromDate,
                    ToDate = alumniprofile.ToDate

                });
            }

            return StatusCode(404);
        }
    }
}