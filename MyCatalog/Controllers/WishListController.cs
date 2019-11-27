using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCatalog.Models;

namespace MyCatalog.Controllers
{
    public class WishListController : Controller
    {
        IWishListService wishListService;

        public WishListController(IWishListService WishListService)
        {
            this.wishListService = WishListService;
        }


        public IActionResult Index()
        {
            var model = wishListService.GetWishListItems(this.HttpContext);
            
            return View(model);
        }

        public IActionResult AddToWishList(string Id)
        {
            wishListService.AddToWishList(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromWishList(string Id)
        {
            wishListService.RemoveFromWishList(this.HttpContext, Id);

            return RedirectToAction("Index");
        }


        //public PartialViewResult WishListSummary()
        //{
        //    var wishListSummary = wishListService.GetWishListSummary(this.HttpContext);

        //    return PartialView(wishListSummary);
        //}
    }
}