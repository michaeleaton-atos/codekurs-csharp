using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrderShop.Core.Interfaces;

namespace OrderShop.Core.Notifications
{
    public class PlainTextFormatter : INotificationFormatter
    {
        public string Format(string subject, string body)
        {
            return $"{subject.ToUpperInvariant()}\n\n{body}\n\n--\nIhr OrderShop-Team";
        }
    }
}
