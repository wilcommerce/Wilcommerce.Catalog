using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductImageAddedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public string Name { get; private set; }

        public string OriginalName { get; private set; }

        public ProductImageAddedEvent(Guid productId, string name, string originalName)
            : base(productId, typeof(Models.Product))
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
