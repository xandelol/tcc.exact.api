using System.Threading.Tasks;
using exact.api.Data;
using exact.api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace exact.api.Repository
{
    public class UserRepository : BaseRepository<UserEntity>
    {
        public UserRepository(ExactContext dataContext) : base(dataContext)
        {

        }
		
        public async Task<UserEntity> GetUserByToken(string autorization)
        {
            return await _dataContext.Set<UserEntity>().FirstOrDefaultAsync(f => f.Token.Equals(autorization));
        }
    }
}