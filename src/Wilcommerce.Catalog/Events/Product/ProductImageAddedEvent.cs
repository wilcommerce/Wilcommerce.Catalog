using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductImageAddedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public string Name { get; }

        public string OriginalName { get; }

        public ProductImageAddedEvent(Guid productId, string name, string originalName)
            : base()
        {
            ProductId = productId;
            Name = name;
            OriginalName = originalName;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Image {Name} ({OriginalName}) added to product {ProductId}";
        }
    }
}
