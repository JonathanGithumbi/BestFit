using BestFit.Domain.Interfaces;
using BestFit.Infrastructure.Data;
using BestFit.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Infrastructure.Repositories
{
    public class FeaturedContentRepository : Repository<FeaturedContent>,IFeaturedContentRepository
    {
        private readonly BestFitDbContext bestFitDbContext;

        public FeaturedContentRepository(BestFitDbContext bestFitDbContext) : base(bestFitDbContext)
        {
            this.bestFitDbContext = bestFitDbContext;
        }
    }
}
