using System;
using Wilcommerce.Core.Common.Domain.Models;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents a tier price
    /// </summary>
    public class TierPrice
    {
        /// <summary>
        /// Get or set the tier price's id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Get or set the start quantity for the tier price
        /// </summary>
        public int FromQuantity { get; set; }

        /// <summary>
        /// Get or set the end quantity for the tier price
        /// </summary>
        public int ToQuantity { get; set; }

        /// <summary>
        /// Get or set the price associated to quantity
        /// </summary>
        public Currency Price { get; set; }

        /// <summary>
        /// Get the related product
        /// </summary>
        public virtual Product Product { get; protected set; }
    }
}
