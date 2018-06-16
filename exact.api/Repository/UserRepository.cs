using System.Collections.Generic;
using System.Threading.Tasks;
using exact.api.Data;
using exact.api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace exact.api.Repository
{
    public class UserRepository : BaseRepository<UserEntity>
    {

        private readonly ExactContext _context;

        public UserRepository(ExactContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        public Task<UserEntity> GetByEmail(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity> GetUserByToken(string autorization)
        {
            return await _dataContext.Set<UserEntity>().FirstOrDefaultAsync(f => f.Token.Equals(autorization));
        }
    }
}