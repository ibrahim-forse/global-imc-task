using GlobalIMCTask.API.ViewModels.DietaryTypes;
using GlobalIMCTask.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalIMCTask.API.ViewModels.Products
{
    public class ProductVMBase
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public double Price { set; get; }
        public string ImageURL { set; get; }
        public string VendorUID { set; get; }
        public int ViewsCount { set; get; }
    }

    public class ProductVM : ProductVMBase
    {
        public List<DietaryTypeVM> DietaryTypes { set; get; }
    }

    public class ProductsTableVM : PaginationVM
    {
        public List<ProductVM> Products { set; get; }
    }

    public class ProductCreationVM : ProductVMBase
    {
        public int[] DietaryTypeIds { set; get; }
    }

    public static class ProductConverters
    {
        public static ProductVM ConvertProductToVM(this Product product)
        {
            return new ProductVM()
            {
                Id = product.Id,
                Description = product.Description,
                ImageURL = product.ImageURL,
                DietaryTypes = product.DietaryTypes.ConvertDietaryTypesToVMs(),
                Price = product.Price,
                Title = product.Title,
                VendorUID = product.VendorUID,
                ViewsCount = product.ViewCount
            };
        }

        public static List<ProductVM> ConvertProductsToVMs(this List<Product> products)
        {
            List<ProductVM> vms = new List<ProductVM>();
            foreach(var product in products)
            {
                vms.Add(product.ConvertProductToVM());
            }
            return vms;
        }
    }
}
