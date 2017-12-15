using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PortfolioController : Controller
    {
        // GET: Portfolio
        public ActionResult Index()
        {
            User currentUser = (User)Session["currentUser"];
            int currentUserId = currentUser.id;
            var entities = new stocktradingEntities();
            IList<Models.Portfolio> portfolios = entities.Portfolios.ToList();
            var filteredResult = from p in portfolios
                                 where p.UserId == currentUserId select p;

            return View(filteredResult.ToList());
        }
    }
}