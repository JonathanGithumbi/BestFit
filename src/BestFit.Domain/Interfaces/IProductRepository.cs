using BestFit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null
            , bool isAscending = true,int pageNumber = 1, int pageSize=10,double fromPrice=0,double toPrice=0);

    }
}
