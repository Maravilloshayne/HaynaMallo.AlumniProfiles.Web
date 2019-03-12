using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayna.AlumniProfileEntries.Web.VIewModels.Account
{
    public class ProfileImageViewModel
    {
        public IFormFile Image { get; set; }
    }
}
