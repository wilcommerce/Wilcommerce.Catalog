using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductImageRemovedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Guid ImageId { get; }

        public ProductImageRemovedEvent(Guid productId, Guid imageId)
            : base()
        {
            ProductId = productId;
            ImageId = imageId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Image {ImageId} removed from product {ProductId}";
        }
    }
}
