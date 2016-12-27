using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandNameNotChangedEvent : DomainEvent
    {
        public Guid BrandId { get; }

        public string Name { get; }

        public string Error { get; }

        public BrandNameNotChangedEvent(Guid brandId, string name, string error)
            : base()
        {
            BrandId = brandId;
            Name = name;
            Error = error;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {Error} changing brand {BrandId} name to {Name}";
        }
    }
}
