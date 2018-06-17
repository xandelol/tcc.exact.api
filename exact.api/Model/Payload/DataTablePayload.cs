namespace exact.common.Model.Payload
{
    public class DataTablePayload
    {
        public string Draw { get; set; }
        
        public int Length { get; set; }
        
        public string SortColumn { get; set; }
        
        public string SortColumnDirection { get; set; }
        
        public string SearchValue { get; set; }
        
        public int PageSize { get; set; }
        
        public int Skip { get; set; }



        public bool IsAsc()
        {
            return SortColumnDirection == "asc";
        }
    }
}