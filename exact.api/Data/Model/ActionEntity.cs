namespace exact.api.Data.Model
{
    public class ActionEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Url { get; set; }

        public string Icon { get; set; }
        
        public string MenuId { get; set; }

        public int Index { get; set; }
    }
}