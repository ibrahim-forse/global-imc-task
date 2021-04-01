using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Core.Models
{
    public class Product
    {
        public int Id { set; get; }
        public string Code { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public double Price { set; get; }
        public string ImageURL { set; get; }
        public virtual List<DietaryType> DietaryTypes { set; get; }
        public int ViewCount { set; get; }
        public string VendorUID { set; get; }
    }
}
