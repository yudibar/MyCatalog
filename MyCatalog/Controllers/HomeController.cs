using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCatalog.Models;

namespace MyCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

      

        IRepository<Product> context;

        private readonly IWebHostEnvironment _hostingEnvironment;


        public HomeController(IRepository<Product> productContext, IWebHostEnvironment hostingEnvironment, ILogger<HomeController> logger)
        {
            context = productContext;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public IActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if(product == null)
            {
                return NotFound();
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
