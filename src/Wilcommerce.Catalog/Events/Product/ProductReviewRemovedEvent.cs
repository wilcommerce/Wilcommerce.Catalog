using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product review removed
    /// </summary>
    public class ProductReviewRemovedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the review id
        /// </summary>
        public Guid ReviewId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="reviewId">The review id</param>
        public ProductReviewRemovedEvent(Guid productId, Guid reviewId)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            ReviewId = reviewId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Review {ReviewId} removed from product {ProductId}";
        }
    }
}
