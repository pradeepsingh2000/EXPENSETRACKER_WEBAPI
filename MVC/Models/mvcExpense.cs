using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcExpense
    {
        
        public int ExpenseId { get; set; }
        [Required(ErrorMessage = "This Field Requied")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This Field Requied")]
        public string Description { get; set; }
      
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public System.DateTime ExpenseDate { get; set; }

        public virtual mvcCategory Category { get; set; }
    }
}