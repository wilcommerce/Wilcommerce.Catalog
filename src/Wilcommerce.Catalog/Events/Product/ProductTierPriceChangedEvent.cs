using System;
using Wilcommerce.Core.Common.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Tier price changed
    /// </summary>
    public class ProductTierPriceChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the tier price id
        /// </summary>
        public Guid TierPriceId { get; private set; }

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
        /// <param name="tierPriceId">The tier price id</param>
        /// <param name="fromQuantity">The start quantity</param>
        /// <param name="toQuantity">The end quantity</param>
        /// <param name="price">The price</param>
        public ProductTierPriceChangedEvent(Guid productId, Guid tierPriceId, int fromQuantity, int toQuantity, Currency price)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            TierPriceId = tierPriceId;
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
            return $"Tier price {TierPriceId} changed to {FromQuantity} - {ToQuantity} ({Price.Code} {Price.Amount}) for product {ProductId}";
        }
    }
}
