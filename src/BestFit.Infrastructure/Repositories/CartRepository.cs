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
    public class CartRepository : Repository<Cart> , ICartRepository
    {
        private readonly BestFitDbContext bestFitDbContext;

        public CartRepository(BestFitDbContext bestFitDbContext) : base(bestFitDbContext)
        {
            this.bestFitDbContext = bestFitDbContext;
        }
    }
}
