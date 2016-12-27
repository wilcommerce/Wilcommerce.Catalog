using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandDescriptionNotChangedEvent : DomainEvent
    {
        public Guid BrandId { get; }

        public string Description { get; }

        public string Error { get; }

        public BrandDescriptionNotChangedEvent(Guid brandId, string description, string error)
            : base()
        {
            BrandId = brandId;
            Description = description;
            Error = error;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {Error} changing {BrandId} description to {Description}";
        }
    }
}
