using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderShop.Core.Interfaces
{
    public interface ICustomerWriter
    {
        void Update(Customer customer);
        void Delete(int id);
    }
}
