using exact.api.Data;
using exact.api.Data.Model;

namespace exact.api.Repository
{
    public class QuestionRespository: BaseRepository<QuestionEntity>
    {
        public QuestionRespository(ExactContext dataContext) : base(dataContext)
        {
        }
    }
}