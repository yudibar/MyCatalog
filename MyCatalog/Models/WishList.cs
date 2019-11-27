using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCatalog.Models
{
    public class WishList : BaseEntity
    {
        public virtual ICollection<WishListItem> WishListItems { get; set; }
        public WishList()
        {
            this.WishListItems = new List<WishListItem>();
        }
    }
}
