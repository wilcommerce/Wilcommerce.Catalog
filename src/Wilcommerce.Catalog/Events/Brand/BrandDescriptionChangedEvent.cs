using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandDescriptionChangedEvent : DomainEvent
    {
        public Guid BrandId { get; }

        public string Description { get; }

        public BrandDescriptionChangedEvent(Guid brandId, string description)
            : base()
        {
            BrandId = brandId;
            Description = description;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {BrandId} change description to {Description}";
        }
    }
}
