using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EXPENSETRACKER_WEBAPI.Models;

namespace EXPENSETRACKER_WEBAPI.Controllers
{
    public class ExpensesController : ApiController
    {
        private ExpenseTrackerDbEntities db = new ExpenseTrackerDbEntities();

        // GET: api/Expenses
        public IHttpActionResult GetExpenses()
        {
            var expenses = db.Expenses.Join(db.Categories, e => e.CategoryId, c => c.CategoryId, (e, c) => new { e, c })
                .Select(ec => new ExpenseViewModel()
                {
                    ExpenseId = ec.e.ExpenseId,
                    Title = ec.e.Title,
                    Description = ec.e.Description,
                    Amount = ec.e.Amount,
                    CategoryName = ec.c.CategoryName,
                    ExpenseDate = ec.e.ExpenseDate,


                }).ToList();

            return Ok(expenses);
        }
        [HttpGet]
        public IHttpActionResult GetExpensesbycategory(int categoryid)
        {
            var expense = db.Expenses.Where(e => e.CategoryId == categoryid).ToList();
                
            return Ok(expense);
        }

       
       
        // GET: api/Expenses/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult GetExpense(int id)
        {   
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        // PUT: api/Expenses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExpense(int id, Expense expense)
        {
            expense.ExpenseDate = DateTime.Now;

            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }
            
           
            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }

            db.Entry(expense).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Expenses
        [ResponseType(typeof(Expense))]
        public IHttpActionResult PostExpense(Expense expense)
        {
            
            expense.ExpenseDate = DateTime.Now;
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Expenses.Add(expense);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = expense.ExpenseId }, expense);
        }

        // DELETE: api/Expenses/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult DeleteExpense(int id)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            db.Expenses.Remove(expense);
            db.SaveChanges();

            return Ok(expense);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExpenseExists(int id)
        {
            return db.Expenses.Count(e => e.ExpenseId == id) > 0;
        }

       
    }
}