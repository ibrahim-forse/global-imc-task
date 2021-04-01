using GlobalIMCTask.Core.Models;
using GlobalIMCTask.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Tests.Mock.Products
{
    public class MockProductsRepository : IProductsRepository
    {
        private List<Product> _db;
        private readonly List<DietaryType> _dts;

        public MockProductsRepository()
        {
            _dts = new List<DietaryType>()
                    {
                        new DietaryType()
                        {
                            Id = 1,
                            Name = "Diet 1"
                        },new DietaryType()
                        {
                            Id = 2,
                            Name = "Diet 2"
                        },
                    };

            _db = new List<Product>()
            {
                new Product()
                {
                    Code = Guid.NewGuid().ToString(),
                    Description = "Description 1",
                    Id = 1,
                    DietaryTypes = new List<DietaryType>(){
                        _dts[0]
                        },
                    ImageURL = "https://www.google.com",
                    Price = 19.0,
                    Title = "Title 1",
                    ViewCount = 10

                }
                ,new Product()
                {
                    Code = Guid.NewGuid().ToString(),
                    Description = "Description 2",
                    Id = 1,
                    DietaryTypes = new List<DietaryType>(){
                        _dts[0], _dts[1]
                        },
                    ImageURL = "https://www.facebook.com",
                    Price = 30.0,
                    Title = "Title 2",
                    ViewCount = 9
                }
                ,new Product()
                {
                    Code = Guid.NewGuid().ToString(),
                    Description = "Description 3",
                    Id = 1,
                    DietaryTypes = new List<DietaryType>(){
                        _dts[1]
                        },
                    ImageURL = "https://www.twitter.com",
                    Price = 30.0,
                    Title = "Title 3",
                    ViewCount = 9
                }
                ,new Product()
                {
                    Code = Guid.NewGuid().ToString(),
                    Description = "Description 4",
                    Id = 1,
                    DietaryTypes = new List<DietaryType>(){
                        _dts[1]
                        },
                    ImageURL = "https://www.instagram.com",
                    Price = 30.0,
                    Title = "Title 4",
                    ViewCount = 9
                }
            };
        }

        public void CreateProduct(Product product)
        {
            _db.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _db.Remove(product);
        }

        public List<DietaryType> GetDietaryTypes(int[] typeIds)
        {
            return _dts
                .Where(d => typeIds.Contains(d.Id))
                .ToList();
        }

        public List<DietaryType> GetDietaryTypes()
        {
            return _dts.ToList();
        }

        public Product GetProduct(int id)
        {
            return _db
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public Tuple<List<Product>, int> GetProducts(int page, int pageSize)
        {
            int count = _db.Count();
            var results=  _db
                .OrderByDescending(p => p.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return Tuple.Create(results, count);

        }

        public Tuple<List<Product>, int> FindProductByTitle(string title, int page, int pageSize)
        {
            int count = _db
                .Where(p => p.Title.ToLower().Contains(title.ToLower())).Count();
            var results= _db
                .Where(p => p.Title.ToLower().Contains(title.ToLower()))
                .OrderByDescending(p => p.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return Tuple.Create(results, count);
        }

        public Tuple<List<Product>, int> FindProductByDescription(string description, int page, int pageSize)
        {
            int count = _db
                .Where(p => p.Description.ToLower().Contains(description.ToLower())).Count();
            var results = _db
                .Where(p => p.Description.ToLower().Contains(description.ToLower()))
                .OrderByDescending(p => p.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            return Tuple.Create(results, count);

        }
    }
}
