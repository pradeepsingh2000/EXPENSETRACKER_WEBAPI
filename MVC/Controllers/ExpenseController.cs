using Antlr.Runtime.Misc;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: Expense
        public ActionResult Index()
        {
            try
            {
                IEnumerable<mvcCategory> catlist;
                HttpResponseMessage responses = GlobalVariable.webapiClient.GetAsync("Categories").Result;
                catlist = responses.Content.ReadAsAsync<IEnumerable<mvcCategory>>().Result;
                ViewBag.category = catlist;
                IEnumerable<ExpenseViewModel> explist;
                HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Expenses").Result;
                explist = response.Content.ReadAsAsync<IEnumerable<ExpenseViewModel>>().Result;
                return View(explist);
            }
            catch
            {
                return View();
            }
        }
        
        public  decimal expenseamount()
        {
            IEnumerable<ExpenseViewModel> explist;
            HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Expenses").Result;
            explist = response.Content.ReadAsAsync<IEnumerable<ExpenseViewModel>>().Result;
            var limit= explist.Sum(e => e.Amount);
            return limit;
        } 

        public ActionResult amount()
        {
            var exp=expenseamount();
            ViewBag.amount = exp;
            return View();
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            IEnumerable<mvcCategory> catlist;
            HttpResponseMessage responses = GlobalVariable.webapiClient.GetAsync("Categories").Result;
            catlist = responses.Content.ReadAsAsync<IEnumerable<mvcCategory>>().Result;
            ViewBag.category = catlist;

            if (id == 0)
            { 
                return View(new mvcExpense());

            }
            else
            {
                HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Expenses/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcExpense>().Result);

            }
        }

        [HttpPost]

        public ActionResult AddOrEdit(mvcExpense exp)
        {
            try
            {
                IEnumerable<mvcCategory> catlist;
                HttpResponseMessage responses = GlobalVariable.webapiClient.GetAsync("Categories").Result;
                catlist = responses.Content.ReadAsAsync<IEnumerable<mvcCategory>>().Result;

                if (exp.ExpenseId == 0)
                {

                    HttpResponseMessage response = GlobalVariable.webapiClient.PostAsJsonAsync("Expenses", exp).Result;
                    TempData["SuccesMessage"] = "Saved SuccessFully";
                    Check();

                }
                else
                {
                    HttpResponseMessage response = GlobalVariable.webapiClient.PutAsJsonAsync("Expenses/" + exp.ExpenseId, exp).Result;
                    TempData["SuccesMessage"] = "Updated SuccessFully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
       
        public ActionResult Delete(int id)
        {
            try
            {
                HttpResponseMessage response = GlobalVariable.webapiClient.DeleteAsync("Expenses/" + id.ToString()).Result;
                return RedirectToAction("Index");
            }
            catch { return View(); }    
        }
        public decimal getbudgetamount()
        {
            IEnumerable<mvcBudget> budget;
            HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Expenses").Result;
            budget = response.Content.ReadAsAsync<IEnumerable<mvcBudget>>().Result;
            var budgetlimit = budget.Sum(e => e.Money_Limit);
            return budgetlimit;

        }
        public string Check()
        {
            var budgetlimit = getbudgetamount();
            var expenselimit = expenseamount();
            string msg = " ";
            if (budgetlimit > expenselimit)
            {
                msg = " expense is low";
            }
            else if (budgetlimit < expenselimit)
            {
                msg = "Expense are grater then Limit";

            }
            else
            {
                 msg = "";
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

    }
}
