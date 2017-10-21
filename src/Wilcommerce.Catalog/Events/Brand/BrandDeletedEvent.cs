using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    /// <summary>
    /// Brand deleted
    /// </summary>
    public class BrandDeletedEvent : DomainEvent
    {
        /// <summary>
        /// Get the brand id
        /// </summary>
        public Guid BrandId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="brandId">The brand id</param>
        public BrandDeletedEvent(Guid brandId)
            : base(brandId, typeof(Models.Brand))
        {
            BrandId = brandId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Brand {BrandId} deleted successfully!";
        }
    }
}
