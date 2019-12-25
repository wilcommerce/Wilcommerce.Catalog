using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product category added
    /// </summary>
    public class ProductCategoryAddedEvent : DomainEvent
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
        /// Get whether the category is a main category
        /// </summary>
        public bool IsMain { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        /// <param name="isMain">Whether the category is a main category</param>
        public ProductCategoryAddedEvent(Guid productId, Guid categoryId, bool isMain)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            CategoryId = categoryId;
            IsMain = isMain;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Category {CategoryId} added to product {ProductId}";
        }
    }
}
