using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product unit in stock changed
    /// </summary>
    public class ProductUnitInStockChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the unit in stock
        /// </summary>
        public int UnitInStock { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="unitInStock">The unit in stock</param>
        public ProductUnitInStockChangedEvent(Guid productId, int unitInStock)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            UnitInStock = unitInStock;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} unit in stock changed to {UnitInStock}";
        }
    }
}
