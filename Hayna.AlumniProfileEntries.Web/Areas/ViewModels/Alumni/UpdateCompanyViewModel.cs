using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayna.AlumniProfileEntries.Web.Areas.ViewModels.Alumni
{
    public class UpdateCompanyViewModel
    {
        public Guid? AlumniProfileId { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }


    }
}