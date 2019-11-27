using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCatalog.Models;
using MyCatalog.InMemory;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNet.Http;
using Grpc.Core;
using Glimpse.AspNet.Tab;

namespace MyCatalog.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;

        private readonly IWebHostEnvironment _hostingEnvironment;

   
        public ProductManagerController(IRepository<Product> productContext, IWebHostEnvironment hostingEnvironment)
        {
            context = productContext;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public IActionResult Create(Product product, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    //var path = Path.Combine(_hostingEnvironment.ContentRootPath, "/Content//ProductImages//" + product.Image);

                    var path1 = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                   
                    var path = Path.Combine(path1, product.Image);

                    file.CopyTo(new FileStream(path, FileMode.Create));
                    //using (var fileStream = new FileStream(path, FileMode.Create)
                    //{
                    //     await file.CopyToAsync(fileStream).ConfigureAwait(false);
                    //}
                    

                }
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]

        public IActionResult Edit(Product product, string Id, IFormFile file)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return NotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }


                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);          
                    var path1 = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                    var path = Path.Combine(path1, productToEdit.Image);
                    file.CopyTo(new FileStream(path, FileMode.Create));
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                
                productToEdit.Name = product.Name;

                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }


    }
}