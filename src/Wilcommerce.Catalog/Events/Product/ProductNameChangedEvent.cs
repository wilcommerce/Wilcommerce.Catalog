using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductNameChangedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public string Name { get; }

        public ProductNameChangedEvent(Guid productId, string name)
            : base()
        {
            ProductId = productId;
            Name = name;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} name changed to {Name}";
        }
    }
}
