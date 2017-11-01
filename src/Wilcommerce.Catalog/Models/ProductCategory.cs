using System;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represent an association between product and category
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// Get or set the product id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Get or set the category id
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Get or set whether the category is the main category
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// Get the product instance
        /// </summary>
        public virtual Product Product { get; protected set; }

        /// <summary>
        /// Get the category instance
        /// </summary>
        public virtual Category Category { get; protected set; }
    }
}
