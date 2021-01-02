using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product review approved
    /// </summary>
    public class ProductReviewApprovedEvent : DomainEvent
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
        /// <param name="userId">The user's id</param>
        public ProductReviewApprovedEvent(Guid productId, Guid reviewId, string userId)
            : base(productId, typeof(Models.Product), userId)
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
            return $"Review {ReviewId} approved for product {ProductId}";
        }
    }
}
