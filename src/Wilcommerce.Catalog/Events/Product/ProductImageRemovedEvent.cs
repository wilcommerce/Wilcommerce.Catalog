using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product image removed
    /// </summary>
    public class ProductImageRemovedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the image id
        /// </summary>
        public Guid ImageId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="imageId">The image id</param>
        public ProductImageRemovedEvent(Guid productId, Guid imageId)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            ImageId = imageId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Image {ImageId} removed from product {ProductId}";
        }
    }
}
