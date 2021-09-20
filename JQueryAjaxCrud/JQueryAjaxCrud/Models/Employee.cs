using System.ComponentModel.DataAnnotations;

namespace JQueryAjaxCrud.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Provied Name.")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set;  }

        [Required(ErrorMessage = "Provied Email.")]
        [StringLength(50, MinimumLength = 11)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Provied Department Name.")]
        [Display(Name = "Department Name")]
        [StringLength(20, MinimumLength = 2)]
        public string DepartmentName { get; set;  }

        [Required(ErrorMessage = "Provied Account Number.")]
        [Display(Name = "Account Number")]
        [StringLength(20, MinimumLength = 8)]
        [DataType(DataType.PhoneNumber)]
        public string AccountNumber { get; set; }
    }
}
