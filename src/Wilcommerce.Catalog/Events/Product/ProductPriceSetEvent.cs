using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product price set
    /// </summary>
    public class ProductPriceSetEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the product price
        /// </summary>
        public Currency Price { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="price">The product price</param>
        public ProductPriceSetEvent(Guid productId, Currency price)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            Price = price;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} price set to {Price.Code} {Price.Amount.ToString()}";
        }
    }
}
