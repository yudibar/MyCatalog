using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCatalog.Models
{
    public class WishListItem : BaseEntity
    {
        public string WishListId { get; set; }
        public string ProductId { get; set; }
        public int Quanity { get; set; }

    }
}
