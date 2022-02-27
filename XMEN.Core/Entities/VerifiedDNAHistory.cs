
namespace XMEN.Core.Entities
{
    public class VerifiedDNAHistory : BaseEntity
    {
        public int Id { get; set; }
        public string DNA { get; set; }
        public bool IsMutant { get; set; }
    }
}
