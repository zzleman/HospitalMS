using ZeusMed.Core.Interfaces;

namespace ZeusMed.Core.Entities
{
    public class ServiceDetail : IEntity
    {
        public int Id { get; set; }

        public string? Info { get; set; }

        public Service Service { get; set; }
    }
}
