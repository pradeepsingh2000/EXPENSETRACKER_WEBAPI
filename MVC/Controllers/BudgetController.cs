using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class BudgetController : Controller
    {
        // GET: Budget
        public ActionResult Index()
        {
            try
            {

                IEnumerable<mvcBudget> budgetlist;
                HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Budgets").Result;
                budgetlist = response.Content.ReadAsAsync<IEnumerable<mvcBudget>>().Result;

                return View(budgetlist);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new mvcBudget());

            }
            else
            {
                HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Budgets/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcBudget>().Result);

            }
        }
        [HttpPost]

        public ActionResult AddOrEdit(mvcBudget bug)
        {
            try
            {
                if (bug.BudgetId == 0)
                {
                    HttpResponseMessage response = GlobalVariable.webapiClient.PostAsJsonAsync("Budgets", bug).Result;
                    TempData["SuccesMessage"] = "Saved SuccessFully";

                }
                else
                {
                    HttpResponseMessage response = GlobalVariable.webapiClient.PutAsJsonAsync("Budgets/" + bug.BudgetId, bug).Result;
                    TempData["UpdateMessage"] = "Updated SuccessFully";
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

                HttpResponseMessage response = GlobalVariable.webapiClient.DeleteAsync("Budgets/" + id.ToString()).Result;
                return RedirectToAction("Index");
            }
            catch { return View(); }    
        }



    }
}