using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandUrlChangedEvent : DomainEvent
    {
        public Guid BrandId { get; }

        public string Url { get; }

        public BrandUrlChangedEvent(Guid brandId, string url)
            : base()
        {
            BrandId = brandId;
            Url = url;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {BrandId} change url to {Url}";
        }
    }
}
