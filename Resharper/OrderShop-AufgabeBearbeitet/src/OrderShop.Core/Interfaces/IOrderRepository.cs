using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderShop.Core.Interfaces
{
    public interface IOrderRepository
    {
        int Save(Order order);
        Order? GetById(int id);
    }
}
