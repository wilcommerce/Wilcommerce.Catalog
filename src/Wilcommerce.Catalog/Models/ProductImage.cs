using System;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents a product image
    /// </summary>
    public class ProductImage
    {
        /// <summary>
        /// Get or set the image's id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Get or set the image's full path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Get or set the image's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the image's original name
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// Get or set whether the image is the product's main image
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// Get or set the date and time of when the image is uploaded
        /// </summary>
        public DateTime? UploadedOn { get; set; }

        /// <summary>
        /// Get the related product
        /// </summary>
        public virtual Product Product { get; protected set; }
    }
}
