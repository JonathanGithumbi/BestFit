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
    public class ApplicationUserRepository : Repository<ApplicationUser>,IApplicationUserRepository  //Cretae a class, inheriting from a base class and implementing an interface
    {
        private  BestFitDbContext _bestFitDbContext;

        public ApplicationUserRepository(BestFitDbContext bestFitDbContext) : base(bestFitDbContext)
        {
            _bestFitDbContext = bestFitDbContext;
        }
    }
}
