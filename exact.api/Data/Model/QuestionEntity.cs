namespace exact.api.Data.Model
{
    public class QuestionEntity : BaseEntity    {

            /// <summary>
            ///     The question statement
            /// </summary>
            public string Statement { get; set; }

            /// <summary>
            ///  The question's answer
            /// </summary>
            public int Answer { get; set; }
     
    }
}