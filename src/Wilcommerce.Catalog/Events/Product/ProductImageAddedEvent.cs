using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product image added
    /// </summary>
    public class ProductImageAddedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the image's name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the image's original name
        /// </summary>
        public string OriginalName { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="name">The image's name</param>
        /// <param name="originalName">The image's original name</param>
        public ProductImageAddedEvent(Guid productId, string name, string originalName)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            Name = name;
            OriginalName = originalName;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Image {Name} ({OriginalName}) added to product {ProductId}";
        }
    }
}
