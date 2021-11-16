using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using QuarterlySales.Models.Validation;

namespace QuarterlySales.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [Display(Name = "First Name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        [Display(Name = "Last Name")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a birth date.")]
        [PastDate(ErrorMessage = "Birth date must be a valid date that's in the past."]
        [Display(Name = "Birth Date")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter a hire date.")]
        [PastDate(ErrorMessage = "Hire date must be a valid date that's in the past."]
        [GreaterThan("1/1/1995", ErrorMessage = "Hire date can't be before company was formed in 1995.")]
        [Display(Name = "Hire Date")]
        public DateTime? DateOfHire { get; set; }

        [GreaterThan(0, ErrorMessage = "Please select a manager.")]
        [Display(Name = "Manager")]
        public int ManagerId { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
