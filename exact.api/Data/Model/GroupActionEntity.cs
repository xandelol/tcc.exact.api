using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace exact.api.Data.Model
{
    public class GroupActionEntity : BaseEntity
    {
        public Guid GroupId { get; set; }
        
        public Guid ActionId { get; set; }
        
        [ForeignKey("GroupId")]
        public GroupEntity Group { get; set; }
        
        [ForeignKey("ActionId")]
        public ActionEntity Action { get; set; }
    }
}