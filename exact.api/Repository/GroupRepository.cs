using exact.api.Data;

namespace exact.api.Repository
{
    public class GroupRepository : BaseRepository<GroupEntity>
    {
        public GroupRepository(ExactContext dataContext) : base(dataContext)
        {

        }
		
    }
}