using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandRestoredEvent : DomainEvent
    {
        public Guid BrandId { get; private set; }

        public BrandRestoredEvent(Guid brandId)
            : base(brandId, typeof(Models.Brand))
        {
            BrandId = brandId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Brand {BrandId} restored successfully";
        }
    }
}
