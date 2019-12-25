using System;
using Wilcommerce.Core.Common.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Tier price added to product
    /// </summary>
    public class ProductTierPriceAddedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the start quantity of the tier price
        /// </summary>
        public int FromQuantity { get; private set; }

        /// <summary>
        /// Get the end quantity of the tier price
        /// </summary>
        public int ToQuantity { get; private set; }

        /// <summary>
        /// Get the price
        /// </summary>
        public Currency Price { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="fromQuantity">The start quantity</param>
        /// <param name="toQuantity">The end quantity</param>
        /// <param name="price">The price</param>
        public ProductTierPriceAddedEvent(Guid productId, int fromQuantity, int toQuantity, Currency price)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            FromQuantity = fromQuantity;
            ToQuantity = toQuantity;
            Price = price;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Tier price {FromQuantity} - {ToQuantity} ({Price.Code} {Price.Amount}) added to product {ProductId}";
        }
    }
}
