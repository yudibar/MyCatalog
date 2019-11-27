using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCatalog.View_Model
{
    public class WishListSummaryViewModel
    {

        public int WishListCount { get; set; }

        public WishListSummaryViewModel()
        {

        }

        public WishListSummaryViewModel(int wishListCount)
        {
            this.WishListCount = wishListCount;
        }
    }
}
