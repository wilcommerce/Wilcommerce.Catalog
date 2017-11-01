using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product description changed
    /// </summary>
    public class ProductDescriptionChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the product description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="description">The product description</param>
        public ProductDescriptionChangedEvent(Guid productId, string description)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            Description = description;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} description changed to {Description}";
        }
    }
}
