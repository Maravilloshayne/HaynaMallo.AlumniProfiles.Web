using Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Helpers;
using Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayna.AlumniProfileEntries.Web.Areas.ViewModels.Alumni
{
    public class IndexViewModel
    {
        public Page<AlumniProfile> AlumniProfiles { get; set; }
    }
}