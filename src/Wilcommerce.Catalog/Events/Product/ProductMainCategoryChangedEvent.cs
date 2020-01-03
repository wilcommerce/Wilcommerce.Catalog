using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product main category changed
    /// </summary>
    public class ProductMainCategoryChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }
        
        /// <summary>
        /// Get the category id
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        public ProductMainCategoryChangedEvent(Guid productId, Guid categoryId)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            CategoryId = categoryId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Category {CategoryId} set as the new main category for product {ProductId}";
        }
    }
}
