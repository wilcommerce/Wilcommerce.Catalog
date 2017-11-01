using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product's SKU changed
    /// </summary>
    public class ProductSkuChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the product SKU
        /// </summary>
        public string Sku { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="sku">The product SKU</param>
        public ProductSkuChangedEvent(Guid productId, string sku)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            Sku = sku;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} SKU changed to {Sku}";
        }
    }
}
