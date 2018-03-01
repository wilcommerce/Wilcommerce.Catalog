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

        /// <summary>
        /// Get or set whether the review is approved
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// Get or set the date and time of when the review is approved
        /// </summary>
        public DateTime? ApprovedOn { get; set; }

        /// <summary>
        /// Get or the creation date
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Get the related product
        /// </summary>
        public virtual Product Product { get; protected set; }
    }
}
