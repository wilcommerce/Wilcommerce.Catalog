using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product EAN code changed
    /// </summary>
    public class ProductEanCodeChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the EAN code
        /// </summary>
        public string EanCode { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="ean">The EAN code</param>
        public ProductEanCodeChangedEvent(Guid productId, string ean)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            EanCode = ean;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} EAN changed to {EanCode}";
        }
    }
}
