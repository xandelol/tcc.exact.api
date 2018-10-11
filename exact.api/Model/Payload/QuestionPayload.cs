using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exact.api.Model.Payload
{
    public class QuestionPayload : BasePayload
    {
        public string Statement { get; set; }

        public int Answer { get; set; }
    }
}
