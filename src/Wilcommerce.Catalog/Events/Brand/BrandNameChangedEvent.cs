using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandNameChangedEvent : DomainEvent
    {
        public Guid BrandId { get; }

        public string Name { get; }

        public BrandNameChangedEvent(Guid brandId, string name)
            : base()
        {
            BrandId = brandId;
            Name = name;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Brand {BrandId} name changed to {Name}";
        }
    }
}
