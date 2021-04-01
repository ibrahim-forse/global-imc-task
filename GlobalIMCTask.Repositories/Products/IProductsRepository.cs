using GlobalIMCTask.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Repositories.Products
{
    public interface IProductsRepository
    {
        Product GetProduct(int id);
        Tuple<List<Product>, int> GetProducts(int page, int pageSize);
        void DeleteProduct(Product product);
        void CreateProduct(Product product);
        Tuple<List<Product>,int> FindProductByTitle(string title, int page, int pageSize);
        Tuple<List<Product>, int> FindProductByDescription(string description, int page, int pageSize);
        List<DietaryType> GetDietaryTypes(int[] typeIds);
        List<DietaryType> GetDietaryTypes();
    }
}
