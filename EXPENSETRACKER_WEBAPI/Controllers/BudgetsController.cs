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
    public class BudgetsController : ApiController
    {
        private ExpenseTrackerDbEntities db = new ExpenseTrackerDbEntities();

        // GET: api/Budgets
        public IQueryable<Budget> GetBudgets()
        {
            return db.Budgets;
        }

        

        // GET: api/Budgets/5
        [ResponseType(typeof(Budget))]
        public IHttpActionResult GetBudget(int id)
        {
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return NotFound();
            }

            return Ok(budget);
        }

        // PUT: api/Budgets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBudget(int id, Budget budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != budget.BudgetId)
            {
                return BadRequest();
            }

            db.Entry(budget).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(id))
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

        // POST: api/Budgets
        [ResponseType(typeof(Budget))]
        public IHttpActionResult PostBudget(Budget budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Budgets.Add(budget);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = budget.BudgetId }, budget);
        }

        // DELETE: api/Budgets/5
        [ResponseType(typeof(Budget))]
        public IHttpActionResult DeleteBudget(int id)
        {
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return NotFound();
            }

            db.Budgets.Remove(budget);
            db.SaveChanges();

            return Ok(budget);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BudgetExists(int id)
        {
            return db.Budgets.Count(e => e.BudgetId == id) > 0;
        }
    }
}