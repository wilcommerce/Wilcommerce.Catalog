using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product url changed
    /// </summary>
    public class ProductUrlChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the product url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="url">The product url</param>
        public ProductUrlChangedEvent(Guid productId, string url)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            Url = url;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} url changed to {Url}";
        }
    }
}
