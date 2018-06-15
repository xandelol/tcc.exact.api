using exact.api.Data;
using exact.api.Data.Model;

namespace exact.api.Repository
{
    public class QuestionRepository: BaseRepository<QuestionEntity>
    {
        public QuestionRepository(ExactContext dataContext) : base(dataContext)
        {
        }

    }
}