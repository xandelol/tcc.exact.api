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

        public async Task<IEnumerable<UserEntity>> GetById(IEnumerable<int> ids)
        {
            return await _context.Users.Include(u => u.GroupId).Where(u => ids.Contains(u.Id)).ToListAsync();
        }

        public UserEntity GetById(int id)
        {
            return _context.Users.Include(i => i.Id).FirstOrDefault(w => w.Id == id);
        }

        public async Task<UserEntity> GetUserByToken(string autorization)
        {
            return await _dataContext.Set<UserEntity>().FirstOrDefaultAsync(f => f.Token.Equals(autorization));
        }
    }
}