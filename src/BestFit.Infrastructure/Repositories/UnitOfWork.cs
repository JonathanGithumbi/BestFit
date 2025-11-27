using BestFit.Domain.Interfaces;
using BestFit.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BestFitDbContext bestFitDbContext;

        public UnitOfWork(BestFitDbContext bestFitDbContext)
        {
            this.bestFitDbContext = bestFitDbContext;
        }
        public IApplicationUserRepository ApplicationUserRepository => new ApplicationUserRepository(bestFitDbContext);
        public ICartRepository CartRepository => new CartRepository(bestFitDbContext);
        public ICategoryRepository CategoryRepository => new CategoryRepository(bestFitDbContext);
        public ICustomerMeasurementProfileRepository CustomerMeasurementProfileRepository => new CustomerMeasurementProfileRepository(bestFitDbContext);
        public IOrderDetailsRepository OrderDetailsRepository => new OrderDetailsRepository(bestFitDbContext);
        public IOrderProductRepository OrderProductRepository => new OrderProductRepository(bestFitDbContext);
        public IProductImageRepository ProductImageRepository => new ProductImageRepository(bestFitDbContext);
        public IProductMeasurementProfileRepository ProductMeasurementProfileRepository => new ProductMeasurementProfileRepository(bestFitDbContext);
        public IProductRepository ProductRepository => new ProductRepository(bestFitDbContext);

        public void Dispose()
        {
            bestFitDbContext.Dispose();
        }

        public void Save()
        {
            bestFitDbContext.SaveChanges();
        }
    }
}
