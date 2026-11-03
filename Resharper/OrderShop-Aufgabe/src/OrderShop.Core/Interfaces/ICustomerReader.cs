using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderShop.Core.Customers;

namespace OrderShop.Core.Interfaces
{
    public interface ICustomerReader
    {
            Customer? GetById(int id);

    }
}
