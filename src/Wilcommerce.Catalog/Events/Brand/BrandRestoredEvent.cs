using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    /// <summary>
    /// Brand restored
    /// </summary>
    public class BrandRestoredEvent : DomainEvent
    {
        /// <summary>
        /// Get the brand id
        /// </summary>
        public Guid BrandId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="brandId">The brand id</param>
        /// <param name="userId">The user's id</param>
        public BrandRestoredEvent(Guid brandId, string userId)
            : base(brandId, typeof(Models.Brand), userId)
        {
            BrandId = brandId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Brand {BrandId} restored successfully";
        }
    }
}
