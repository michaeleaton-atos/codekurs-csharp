using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderShop.Core.Interfaces
{
    public interface INotificationFormatter
    {
        string Format(string subject, string body);
    }
}
