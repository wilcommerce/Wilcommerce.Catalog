using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandRestoredEvent : DomainEvent
    {
        public Guid BrandId { get; }

        public BrandRestoredEvent(Guid brandId)
            : base()
        {
            BrandId = brandId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Brand {BrandId} restored successfully";
        }
    }
}
