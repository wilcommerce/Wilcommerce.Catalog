using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductReviewRemovedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Guid ReviewId { get; }

        public ProductReviewRemovedEvent(Guid productId, Guid reviewId)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            ReviewId = reviewId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Review {ReviewId} removed from product {ProductId}";
        }
    }
}
