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

        public IEnumerable<Product> GetAll(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10,double fromPrice = 0,double toPrice=10000)
        {
            var products = bestFitDbContext.Products.AsQueryable();


            //Filtering Name & Description
            if (string.IsNullOrEmpty(filterOn) == false && string.IsNullOrEmpty(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) // Filter on product name and description
                {
                    products= products.Where(x => x.Name.Contains(filterQuery) || x.Description.Contains(filterQuery));
                }
                
            }
            //Filter price
            products = products.Where(x=>x.Price >= fromPrice && x.Price <= toPrice);


            //Sorting
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products =  isAscending ? products.OrderBy(x=>x.Name) : products.OrderByDescending(x=>x.Name);  
                }
                if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    products =  isAscending ? products.OrderBy(x=>x.Price) : products.OrderByDescending(x=>x.Price);  
                }
            }

            //pagination 
            var skipResults = (pageNumber - 1) * pageSize;

            return products.Skip(skipResults).Take(pageSize).AsEnumerable<Product>();
        }

        
    }
}
