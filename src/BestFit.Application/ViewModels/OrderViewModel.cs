using BestFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.ViewModels
{
    public class OrderViewModel
    {
        public OrderProduct  OrderProduct{ get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
