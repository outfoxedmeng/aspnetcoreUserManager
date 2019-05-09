using System;
using System.ComponentModel.DataAnnotations;

namespace Heavy.Web.ViewModels
{
    public class CreateUserViewModel
    {
        [Required, MaxLength(20), Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required, MaxLength(50), RegularExpression(@"^[A-Za-z0-9\u4e00-\u9fa5]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$")]
        public string Email { get; set; }

        [Required, MaxLength(20), DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [MaxLength(20)]
        public string Country { get; set; }
    }
}
