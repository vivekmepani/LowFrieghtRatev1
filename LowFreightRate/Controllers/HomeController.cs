using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using LowFreightRate.Data;
using LowFreightRate.Models.PostQuote;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace LowFreightRate.Controllers
{
    public class HomeController : Controller
    {
       

        private readonly QuoteDbContext _db;

        public HomeController(QuoteDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        [HttpGet, Route("Quote")]
        public IActionResult Quote()
        {
            ViewData["Message"] = "Get Instant Quote.";


            return View();

        }
        [HttpPost, Route("Quote")]
        [ValidateAntiForgeryToken]
        public IActionResult Quote(PostQuote model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                _db.Quotes.Add(model);
                _db.SaveChanges();

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(model);
        }
        public IActionResult OceanFreight()
        {
            ViewData["Message"] = "Ocean Freight Shipping";
            return View();

        }
        public IActionResult FTL()
        {
            ViewData["Message"] = "FTL Freight Shipping";
            return View();

        }
        public IActionResult LTL()
        {
            ViewData["Message"] = "LTL Freight Shipping";
            return View();

        }
        public IActionResult Intermodal()
        {
            ViewData["Message"] = "Intermodal Freight Shipping";
            return View();

        }
        public IActionResult Dashboard(int page = 0)
        {

            ViewData["Message"] = "Requested Quote details";


            var pageSize = 2;
            var totalQuotes = _db.Quotes.Count();
            var totalPages = totalQuotes / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;
            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;
            ViewBag.TotalPage = totalQuotes;
            ViewBag.PageSize = pageSize;

            var posts = _db.Quotes.OrderByDescending(x => x.Posted).Take(5).ToArray();

            ViewData["Message"] = "Requested Quote details";
            return View(posts);

        }

    }
}