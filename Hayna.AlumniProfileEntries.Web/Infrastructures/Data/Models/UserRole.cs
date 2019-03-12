using Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Enums;
using Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayna.AlumniProfileEntries.Web.Infrastructures.Data.Models
{
    public class UserRole : BaseModel
    {
        public Guid? UserId { get; set; }
        public Role Role { get; set; }
    }
}