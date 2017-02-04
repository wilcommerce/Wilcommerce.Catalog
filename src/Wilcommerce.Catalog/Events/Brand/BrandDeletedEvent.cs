using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandDeletedEvent : DomainEvent
    {
        public Guid BrandId { get; }

        public BrandDeletedEvent(Guid brandId)
            : base(brandId, typeof(Models.Brand))
        {
            BrandId = brandId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Brand {BrandId} deleted successfully!";
        }
    }
}
