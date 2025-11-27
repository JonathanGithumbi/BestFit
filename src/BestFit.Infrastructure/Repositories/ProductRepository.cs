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
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        private readonly BestFitDbContext bestFitDbContext;

        public ProductRepository(BestFitDbContext bestFitDbContext) : base(bestFitDbContext) 
        {
            this.bestFitDbContext = bestFitDbContext;
        }
    }
}
