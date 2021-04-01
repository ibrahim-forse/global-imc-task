using GlobalIMCTask.Core.Contexts;
using GlobalIMCTask.Core.Models;
using GlobalIMCTask.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Services.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly TaskContext _db;
        public ProductsRepository(TaskContext db)
        {
            _db = db;
        }

        public void CreateProduct(Product product)
        {
            _db.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _db.Products.Remove(product);
        }

        public Tuple<List<Product>, int> FindProductByDescription(string description, int page, int pageSize)
        {
            int count = _db.Products
                .Include(p => p.DietaryTypes)
                .Where(p => p.Description.ToLower().Contains(description.ToLower())).Count();

            var results = _db.Products
            .Include(p => p.DietaryTypes)
            .Where(p => p.Description.ToLower().Contains(description.ToLower()))
            .OrderByDescending(p => p.Id)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
            return Tuple.Create(results, count);
        }

        public Tuple<List<Product>, int> FindProductByTitle(string title, int page, int pageSize)
        {
            int count = _db.Products
                .Include(p => p.DietaryTypes)
                .Where(p => p.Title.ToLower().Contains(title.ToLower())).Count();

            var results = _db.Products
            .Include(p => p.DietaryTypes)
            .Where(p => p.Title.ToLower().Contains(title.ToLower()))
            .OrderByDescending(p => p.Id)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
            return Tuple.Create(results, count);
        }

        public List<DietaryType> GetDietaryTypes(int[] typeIds)
        {
            return _db.DietaryTypes
                .Where(d => typeIds.Contains(d.Id))
                .ToList();
        }

        public List<DietaryType> GetDietaryTypes()
        {
            return _db.DietaryTypes.ToList();
        }

        public Product GetProduct(int id)
        {
            return _db.Products
                .Include(p => p.DietaryTypes)
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public Tuple<List<Product>, int> GetProducts(int page, int pageSize)
        {
            int count = _db.Products
                .Include(p => p.DietaryTypes).Count();

            var results = _db.Products
                .Include(p => p.DietaryTypes)
                .OrderByDescending(p => p.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return Tuple.Create(results, count);
        }
    }
}
