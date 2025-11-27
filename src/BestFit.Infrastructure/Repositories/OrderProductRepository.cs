using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using BestFit.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Infrastructure.Repositories
{
    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
    {
        private readonly BestFitDbContext bestFitDbContext;

        public OrderProductRepository(BestFitDbContext bestFitDbContext) : base(bestFitDbContext)
        {
            this.bestFitDbContext = bestFitDbContext;
        }


        public void UpdateStatus(Guid id, string orderStatus, string? paymentStatus = null)
        {
            throw new NotImplementedException();
        }
    }
}
