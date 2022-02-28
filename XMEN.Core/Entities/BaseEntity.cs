using System;

namespace XMEN.Core.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedUTC { get; set; }
        public DateTime? UpdatedUTC { get; set; }
    }
}
