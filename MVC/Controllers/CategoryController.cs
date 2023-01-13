using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            try
            {
                IEnumerable<mvcCategory> catlist;
                HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Categories").Result;
                catlist = response.Content.ReadAsAsync<IEnumerable<mvcCategory>>().Result;
                return View(catlist);
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
                return View(new mvcCategory());

            }
            else
            {
                HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Categories/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcCategory>().Result);

            }
        }

        [HttpPost]

        public ActionResult AddOrEdit(mvcCategory cat)
        {
            try
            {
                if (cat.CategoryId == 0)
                {
                    HttpResponseMessage response = GlobalVariable.webapiClient.PostAsJsonAsync("Categories", cat).Result;
                    TempData["SuccesMessage"] = "Saved SuccessFully";
                }
                else
                {
                    HttpResponseMessage response = GlobalVariable.webapiClient.PutAsJsonAsync("Categories/" + cat.CategoryId, cat).Result;
                    TempData["SuccesMessage"] = "Updated SuccessFully";
                }
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        public ActionResult Delete(int id)
        {
            try
            {

                HttpResponseMessage response = GlobalVariable.webapiClient.DeleteAsync("Categories/" + id.ToString()).Result;
                return RedirectToAction("Index");
            }
            catch { return View(); }    
        }


        public ActionResult getCategoryForExpense()
        {
            try
            {

                IEnumerable<mvcCategory> catlist;
                HttpResponseMessage responses = GlobalVariable.webapiClient.GetAsync("Categories").Result;
                catlist = responses.Content.ReadAsAsync<IEnumerable<mvcCategory>>().Result;
                ViewBag.category = catlist;
                return View();
            }
            catch { return View(); }

        }

    

        [HttpGet]
        public ActionResult getexpenseByCategory(int categoryid)
        {
            try
            {
                HttpResponseMessage response = GlobalVariable.webapiClient.GetAsync("Expenses?categoryid=" + categoryid).Result;
                var explist = response.Content.ReadAsAsync<IEnumerable<ExpenseCategory>>().Result;
                return View(explist);
            }
            catch { return View(); }
        }
    }
}
