using BestFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Domain.Interfaces
{
    public interface IOrderProductRepository : IRepository<OrderProduct>
    {
        void UpdateStatus(Guid id,string orderStatus,string? paymentStatus=null);
    }
}
