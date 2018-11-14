using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exact.api.Data.Model;

namespace exact.api.Model.Proxy
{
    public class QuestionProxy : BaseProxy
    {
        public string Statement { get; set; }
        public int Answer { get; set; }

        public QuestionProxy EntityToProxy(QuestionEntity entity) => new QuestionProxy
        {
            Id = entity.Id,
            Statement = entity.Statement,
            Answer = entity.Answer,
            IsActive = entity.IsActive
        };

        public List<QuestionProxy> EntityToProxyList(List<QuestionEntity> entityList)
        {
            var result = new List<QuestionProxy>();

            foreach (var entity in entityList)
            {
                result.Add(EntityToProxy(entity));
            }
            
            return result;
        }
    }
}
