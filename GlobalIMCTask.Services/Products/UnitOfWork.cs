using GlobalIMCTask.Core.Contexts;
using GlobalIMCTask.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Services.Products
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskContext _db;
        public UnitOfWork(TaskContext db)
        {
            _db = db;
            Products = new ProductsRepository(_db);
        }
        public IProductsRepository Products { get; private set; }

        public void Complete()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
