using exact.api.Data;
using exact.api.Data.Model;

namespace exact.api.Repository
{
    public class GroupActionRepository : BaseRepository<GroupActionEntity>
    {
        public GroupActionRepository(ExactContext dataContext) : base(dataContext)
        {

        }
		
    }
}