using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcCategory
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "This Field Requied")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "This Field Requied")]
        public decimal CategoryLimit { get; set; }

        public virtual ICollection<mvcExpense> Expenses { get; set; }
    }
}