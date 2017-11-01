using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product attribute added
    /// </summary>
    public class ProductAttributeAddedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the attribute id
        /// </summary>
        public Guid AttributeId { get; private set; }

        /// <summary>
        /// Get the attribute's value
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="value">The attribute's value</param>
        public ProductAttributeAddedEvent(Guid productId, Guid attributeId, object value)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            AttributeId = attributeId;
            Value = value;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} added to product {ProductId} with value {Value.ToString()}";
        }
    }
}
