using System;

namespace Wilcommerce.Catalog.Models
{
    public class ProductAttribute
    {
        /// <summary>
        /// Get or set the attribute's id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Get or set the attribute's value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Get or set the attribute's product reference
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Get or set the attribute's custom attribute reference
        /// </summary>
        public virtual CustomAttribute Attribute { get; set; }
    }
}
