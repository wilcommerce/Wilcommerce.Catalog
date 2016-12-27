using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandUrlNotChangedEvent : DomainEvent
    {
        public Guid BrandId { get; }

        public string Url { get; }

        public string Error { get; }

        public BrandUrlNotChangedEvent(Guid brandId, string url, string error)
            : base()
        {
            BrandId = brandId;
            Url = url;
            Error = error;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {Error} changing brand {BrandId} url to {Url}";
        }
    }
}
