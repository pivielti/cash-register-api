using System.Collections.Generic;
using System.Linq;
using CashRegister.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Infrastructure.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly CashRegisterContext _context;

        public ProductRepository(CashRegisterContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.Include(x => x.Category).ToList();
        }
    }
}