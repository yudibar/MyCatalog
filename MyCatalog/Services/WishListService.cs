using Microsoft.AspNetCore.Http;
using MyCatalog.Models;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCatalog.View_Model;

namespace MyCatalog.Services
{
    public class WishListService : IWishListService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        IRepository<Product> productContext;
        IRepository<WishList> wishListContext;

        public const string wishListSessionName = "amazonCatalogWishList";

        public WishListService(IRepository<Product> ProductContext, IRepository<WishList> WishListContext, IHttpContextAccessor httpContextAccessor)
        {
            this.productContext = ProductContext;
            this.wishListContext = WishListContext;
            this._httpContextAccessor = httpContextAccessor;
        }
        private WishList GetWishList(HttpContext httpContext, bool createIfNull)
        {
            string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[wishListSessionName];
            
            WishList wishList = new WishList();

            if(!string.IsNullOrEmpty(cookieValue))
            {
                string wishListId = cookieValue;
                wishList = wishListContext.Find(wishListId);
            }
            else
            {
                if (createIfNull)
                {
                    wishList = CreateNewWishList(httpContext);
                }
            }
            return wishList;
        }
        

        private WishList CreateNewWishList(HttpContext httpContext)
        {
            WishList wishList = new WishList();
            wishListContext.Insert(wishList);
            wishListContext.Commit();

            //CookieOptions cookies = new CookieOptions();

            //Response.Cookies.Append("Token", "Token", CookieOptions);

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Append("Token", Result.Value.Token, options);
            //string cookieName = "cookie";
            //HttpContext.Response.Cookies.Append("Id", cookie, options);
            //HttpResponse.Cookies.Append
            _httpContextAccessor.HttpContext.Response.Cookies.Append(wishListSessionName, wishList.Id, options);

            return wishList;


        }

        public void AddToWishList(HttpContext httpContext, string productId)
        {
            WishList wishList = GetWishList(httpContext, true);
            WishListItem item = wishList.WishListItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                item = new WishListItem()
                {
                    WishListId = wishList.Id,
                    ProductId = productId,
                    Quanity = 1
                };
                wishList.WishListItems.Add(item);
            }
            else
            {
                item.Quanity = item.Quanity + 1;
            }
            wishListContext.Commit();
        }

        public void RemoveFromWishList(HttpContext httpContext, string itemId)
        {
            WishList wishList = GetWishList(httpContext, true);
            WishListItem item = wishList.WishListItems.FirstOrDefault(i => i.ProductId == itemId);

            if (item != null)
            {
                wishList.WishListItems.Remove(item);
                wishListContext.Commit();
            }
        }

        public List<WishListItemViewModel> GetWishListItems(HttpContext httpContext)
        {
            WishList wishList = GetWishList(httpContext, false);
            if (wishList != null)
            {
                var results = (from w in wishList.WishListItems
                              join p in productContext.Collection() on w.ProductId equals p.Id
                              select new WishListItemViewModel()
                              {
                                  Id = w.Id,
                                  Quanity = w.Quanity,
                                  ProductName = p.Name,
                                  Image = p.Image

                                  
                              }).ToList();

                return results;
            }
            else
            {
                return new List<WishListItemViewModel>();
            }
        }

        public WishListSummaryViewModel GetWishListSummary(HttpContext httpContext)
        {
            WishList wishList = GetWishList(httpContext, false);
            WishListSummaryViewModel model = new WishListSummaryViewModel(0);
            if (wishList != null)
            {
                int? wishListCount = (from item in wishList.WishListItems
                                      select item.Quanity).Sum();

                model.WishListCount = wishListCount ?? 0;

                return model;
            }
            else
            {
                return model;
            }
        }

        

        
    }
}
