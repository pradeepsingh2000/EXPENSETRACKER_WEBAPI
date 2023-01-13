using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public List<ExpenseViewModel> getExpense()
        {
            IEnumerable<ExpenseViewModel> explist;
            HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Expenses").Result;
            explist = response.Content.ReadAsAsync<IEnumerable<ExpenseViewModel>>().Result;
            return explist.ToList();
        }
        public decimal expenseamount()
        {
            IEnumerable<ExpenseViewModel> explist;
            HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Expenses").Result;
            explist = response.Content.ReadAsAsync<IEnumerable<ExpenseViewModel>>().Result;
            var limit = explist.Sum(e => e.Amount);
            return limit;
        }

        public decimal getbudgetamount()
        {
            IEnumerable<mvcBudget> budget;
            HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Expenses").Result;
            budget = response.Content.ReadAsAsync<IEnumerable<mvcBudget>>().Result;
            var budgetlimit = budget.Sum(e => e.Money_Limit);
            return budgetlimit;

        }

        public PartialViewResult amount()
        {
            try
            {

                var exp = expenseamount();
                ViewBag.amount = exp;
                return PartialView("_ExpenseLimti");
            }
            catch
            {
                return PartialView();
            }
        }
        public PartialViewResult list()
        {
            try
            {
                var exp = getExpense();
                return PartialView("_Expense", exp);
            }
            catch { return PartialView(); } 
        }


        public string Check()
        {
            var budgetlimit = getbudgetamount();
            var expenselimit = expenseamount();
            string msg= " ";
            if (budgetlimit > expenselimit)
            {
                //msg = " expense is low";
            }
            else if (budgetlimit < expenselimit)
            {
                msg = "Expense are grater then Limit";
               
            }
            else
            {
               // msg = "";
            }
            return msg;
        }
        public PartialViewResult validation()
        {
            try
            {
                ViewBag.msg = Check();
                return PartialView("_Validation");
            }
            catch { return PartialView(); }

        }



        public List<mvcCategory> GetDropDown()
        {
            IEnumerable<mvcCategory> catlist;
            HttpResponseMessage responses = GlobalVariable.webapiClient.GetAsync("Categories").Result;
            catlist = responses.Content.ReadAsAsync<IEnumerable<mvcCategory>>().Result;
            return catlist.ToList();

           
        }
     
        public List<mvcBudget> GetBudget()
        {
            IEnumerable<mvcBudget> budgetlist;
            HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Budgets").Result;
            budgetlist = response.Content.ReadAsAsync<IEnumerable<mvcBudget>>().Result;
            return budgetlist.ToList();
        }

        public PartialViewResult TotalExpenseLimit()
        {
            try
            {
                var limit = GetBudget();
                return PartialView("_Limit", limit);
            }
            catch { return PartialView(); }
        }
        public PartialViewResult DropDown()
        {
            try
            {
                var drop = GetDropDown();
                ViewBag.category = drop;
                return PartialView("_DropDown");
            }
            catch { return PartialView(); }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}