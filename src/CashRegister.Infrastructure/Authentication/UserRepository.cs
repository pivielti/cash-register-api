using System.Linq;
using CashRegister.Domain.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Infrastructure.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly CashRegisterContext _dbContext;

        public UserRepository(CashRegisterContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUser(string login)
        {
            return _dbContext.Users
                .Include(x => x.UserRoles)
                .FirstOrDefault(x => x.Login == login);
        }
    }
}