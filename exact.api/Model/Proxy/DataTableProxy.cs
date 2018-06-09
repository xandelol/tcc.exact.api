using System.Collections.Generic;

namespace exact.api.Model.Proxy
{
    public class DataTableProxy<T> 
    {
        public string Draw { get; set; }

        public int RecordsFiltered { get; set; }
        
        public int RecordsTotal { get; set; }
        
        public ICollection<T> Data { get; set; }
        
    }
}