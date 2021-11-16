using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using QuarterlySales.Models.Validation;

namespace QuarterlySales.Models
{
    public class Sales
    {
        public int SalesId { get; set; }

        [Required(ErrorMessage = "Please enter a quarter.")]
        [Range(1, 4, ErrorMessage = "Quarter must be between 1 and 4.")]
        public int? Quarter { get; set; }

        [Required(ErrorMessage = "Please enter a year.")]
        [GreaterThan(2000, ErrorMessage = "Year may not be before 2000.")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Please enter an amount.")]
        [GreaterThan(0.0, ErrorMessage = "Amount must be greater than zero.")]
        public double? Amount { get; set; }

        [GreaterThan(0, ErrorMessage = "Please select an employee.")]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
