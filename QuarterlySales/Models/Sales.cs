using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QuarterlySales.Models
{
    public class Sales
    {
        public int SalesId { get; set; }

        [Required(ErrorMessage = "Please enter a quarter.")]
        [Range(1, 4, ErrorMessage = "Quarter must be between 1 and 4.")]
        public int? Quarter { get; set; }

        [Required(ErrorMessage = "Please enter a year.")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Please enter an amount.")]
        public double? Amount { get; set; }

        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
