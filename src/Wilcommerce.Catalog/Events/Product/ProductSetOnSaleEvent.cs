using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product set on sale
    /// </summary>
    public class ProductSetOnSaleEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        public ProductSetOnSaleEvent(Guid productId)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} set on sale";
        }
    }
}
