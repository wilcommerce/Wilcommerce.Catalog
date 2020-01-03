using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product review added
    /// </summary>
    public class ProductReviewAddedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the reviewer name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the review rating
        /// </summary>
        public int Rating { get; private set; }

        /// <summary>
        /// Get the review comment
        /// </summary>
        public string Comment { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="name">The reviewer name</param>
        /// <param name="rating">The review rating</param>
        /// <param name="comment">The review comment</param>
        public ProductReviewAddedEvent(Guid productId, string name, int rating, string comment)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            Name = name;
            Rating = rating;
            Comment = comment; 
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"{Name} added a review with rating {Rating} and comment {Comment} for product {ProductId}";
        }
    }
}
