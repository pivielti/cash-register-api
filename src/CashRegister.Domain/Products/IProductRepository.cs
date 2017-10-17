using System.Collections.Generic;

namespace CashRegister.Domain.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
    }
}