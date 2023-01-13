using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ExpenseCategory
    {
      
        public int CategoryId { get; set; }
        public int ExpenseId { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime ExpenseDate { get; set; }

        public virtual mvcCategory Category { get; set; }
        public virtual mvcExpense Expenses { get; set; }

    }
}