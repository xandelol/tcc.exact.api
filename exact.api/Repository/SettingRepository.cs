using exact.api.Data;
using exact.api.Data.Model;

namespace exact.api.Repository
{
    public class SettingRepository: BaseRepository<SettingEntity>
    {
        public SettingRepository(ExactContext dataContext) : base(dataContext)
        {
        }
    }
}