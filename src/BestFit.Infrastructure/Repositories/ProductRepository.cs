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

        public IEnumerable<Product> GetAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var products = bestFitDbContext.Products.AsQueryable();


            //Filtering
            if (string.IsNullOrEmpty(filterOn) == false && string.IsNullOrEmpty(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products= products.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products =  isAscending ? products.OrderBy(x=>x.Name) : products.OrderByDescending(x=>x.Name);  
                }
            }

            return products.AsEnumerable<Product>();
        }

        
    }
}
