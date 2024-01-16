using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Models
{
    [Table("AppUser", Schema = "Security")]
    public class ApplicationUser:IdentityUser
    {
        public bool IsDeleted { get; set; }
        public Patient Patient { get; set; }
    }
}
