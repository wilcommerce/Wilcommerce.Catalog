using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandCreatedEvent : DomainEvent
    {
        public Guid BrandId { get; }

        public string Name { get; }

        public BrandCreatedEvent(Guid brandId, string name)
            : base()
        {
            BrandId = brandId;
            Name = name;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Brand {Name} [{BrandId}] created successfully";
        }
    }
}
