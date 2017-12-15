using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        public ActionResult Index()
        {
            User currentUser = (User)Session["currentUser"];
            int currentUserId = currentUser.id;
            var entities = new stocktradingEntities();
            IList<Models.Transaction> transactions = entities.Transactions.ToList();
            var filteredResult = from t in transactions
                                 where t.UserId == currentUserId
                                 select t;

            return View(filteredResult.ToList());
        }
    }
}