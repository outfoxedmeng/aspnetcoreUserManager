using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(20)]
        public string Country { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
    }
}
