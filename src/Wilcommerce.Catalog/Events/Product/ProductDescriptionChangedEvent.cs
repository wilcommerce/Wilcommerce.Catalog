using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductDescriptionChangedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public string Description { get; private set; }

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
