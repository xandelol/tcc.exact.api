using System;
using System.ComponentModel.DataAnnotations;

namespace exact.api.Data.Model
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}