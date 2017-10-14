using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductNameChangedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public string Name { get; private set; }

        public ProductNameChangedEvent(Guid productId, string name)
            : base(productId, typeof(Models.Product))
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
