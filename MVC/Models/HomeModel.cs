using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class HomeModel
    {
        public int ExpenseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string CategoryName { get; set; }
        public System.DateTime ExpenseDate { get; set; }

    }
}