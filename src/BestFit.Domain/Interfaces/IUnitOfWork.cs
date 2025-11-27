using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Domain.Interfaces
{
    public interface IUnitOfWork  : IDisposable
    {
        IApplicationUserRepository ApplicationUserRepository { get; }
        ICartRepository CartRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IOrderProductRepository OrderProductRepository { get; }  
        IProductRepository ProductRepository { get; }
        ICustomerMeasurementProfileRepository CustomerMeasurementProfileRepository { get; }
        IProductMeasurementProfileRepository ProductMeasurementProfileRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        void Save();
    }
}
