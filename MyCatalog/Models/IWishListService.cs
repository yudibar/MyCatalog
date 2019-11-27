using Microsoft.AspNetCore.Http;
using MyCatalog.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCatalog.Models
{
    public interface IWishListService
    {
        void AddToWishList(HttpContext httpContext, string productId);
        void RemoveFromWishList(HttpContext httpContext, string itemId);
        List<WishListItemViewModel> GetWishListItems(HttpContext httpContext);

        WishListSummaryViewModel GetWishListSummary(HttpContext httpContext);


    }
}
