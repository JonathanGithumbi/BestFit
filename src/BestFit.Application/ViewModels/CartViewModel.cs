using BestFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.ViewModels
{
    public class CartViewModel
    {
        public OrderProduct OrderProduct { get; set; }
        public IEnumerable<Cart> ListCart { get; set; }
    }
}
