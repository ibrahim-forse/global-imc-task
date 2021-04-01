using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Core.Models
{
    public class DietaryType
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public virtual List<Product> Products { set; get; }
    }
}
