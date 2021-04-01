using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Repositories.Products
{
    public interface IUnitOfWork : IDisposable
    {
        IProductsRepository Products { get; }
        void Complete();
    }
}
