using GlobalIMCTask.Core.Contexts;
using GlobalIMCTask.Repositories.Products;
using GlobalIMCTask.Tests.Mock.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Tests.Mock.Products
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public MockUnitOfWork()
        {
            Products = new MockProductsRepository();
        }
        public IProductsRepository Products { get; private set; }

        public void Complete()
        {
        }

        public void Dispose()
        {
        }
    }
}
