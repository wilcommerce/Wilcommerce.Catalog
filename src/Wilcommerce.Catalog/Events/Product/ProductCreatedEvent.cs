using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product created
    /// </summary>
    public class ProductCreatedEvent : DomainEvent
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
        /// Get the product's SKU
        /// </summary>
        public string Sku { get; private set; }

        /// <summary>
        /// Get the product's name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="ean">The EAN code</param>
        /// <param name="sku">The SKU code</param>
        /// <param name="name">The product's name</param>
        public ProductCreatedEvent(Guid productId, string ean, string sku, string name)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            EanCode = ean;
            Sku = sku;
            Name = name;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} created with EAN {EanCode}, SKU {Sku} and name {Name}";
        }
    }
}
