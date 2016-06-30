using System;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents a product review
    /// </summary>
    public class ProductReview
    {
        /// <summary>
        /// Get or set the product review id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Get or set the name of the user who add the review
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the rating given to the product
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Get or set the comment given to the product
        /// </summary>
        public string Comment { get; set; }
    }
}
