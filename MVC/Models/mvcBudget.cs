using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcBudget
    {
        [Required(ErrorMessage ="This Field Requied")]
        public int BudgetId { get; set; }
        [Required(ErrorMessage = "This Field Requied")]
        public string BudgetName { get; set; }
        [Required(ErrorMessage = "This Field Requied")]
        public decimal Money_Limit { get; set; }
    }
}