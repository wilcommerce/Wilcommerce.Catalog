using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductReviewApprovedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Guid ReviewId { get; }

        public ProductReviewApprovedEvent(Guid productId, Guid reviewId)
            : base()
        {
            ProductId = productId;
            ReviewId = reviewId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Review {ReviewId} approved for product {ProductId}";
        }
    }
}
