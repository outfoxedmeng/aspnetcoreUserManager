using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Web.ViewModels
{
    public class RoleCreateViewModel
    {

        [Display(Name = "Role Name"), Required, MaxLength(50)]
        public string RoleName { get; set; }
    }
}
