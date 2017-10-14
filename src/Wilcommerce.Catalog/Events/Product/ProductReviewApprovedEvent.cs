using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductReviewApprovedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public Guid ReviewId { get; private set; }

        public ProductReviewApprovedEvent(Guid productId, Guid reviewId)
            : base(productId, typeof(Models.Product))
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
