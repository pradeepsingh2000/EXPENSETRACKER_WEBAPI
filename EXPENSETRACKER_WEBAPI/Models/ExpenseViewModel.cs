using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EXPENSETRACKER_WEBAPI.Models
{
    public class ExpenseViewModel
    {
        public int ExpenseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string CategoryName { get; set; }
        public System.DateTime ExpenseDate { get; set; }

        public virtual Category Category { get; set; }
    }
}