using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductDescriptionChangedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public string Description { get; }

        public ProductDescriptionChangedEvent(Guid productId, string description)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            Description = description;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} description changed to {Description}";
        }
    }
}
