using System.Collections.Generic;
using OrderShop.Core.Customers;

namespace OrderShop.Core.Interfaces
{
    /// <summary>
    /// Fachlicher Zugriff auf die Kundendaten. Enthält bewusst keine
    /// technischen Begriffe (keine Tabellen-, Record- oder SQL-Details) -
    /// die konkrete Speicherung bleibt Sache der Infrastructure-Schicht.
    /// </summary>
    public interface ICustomerRepository : ICustomerReader, ICustomerSearch, ICustomerWriter
    {
        IReadOnlyList<Customer> GetAll();
    }
}
