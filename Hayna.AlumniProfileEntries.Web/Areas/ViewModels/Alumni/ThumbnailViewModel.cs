using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayna.AlumniProfileEntries.Web.Areas.ViewModels.Alumni
{
    public class ThumbnailViewModel
    {
        public Guid? AlumniProfileId {get; set; }

        public IFormFile Thumbnail { get; set; }
    }
}