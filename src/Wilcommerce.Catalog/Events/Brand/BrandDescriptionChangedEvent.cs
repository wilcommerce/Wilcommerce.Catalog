using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    /// <summary>
    /// Brand description changed
    /// </summary>
    public class BrandDescriptionChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the brand id
        /// </summary>
        public Guid BrandId { get; private set; }

        /// <summary>
        /// Get the brand description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="brandId">The brand id</param>
        /// <param name="description">The brand description</param>
        public BrandDescriptionChangedEvent(Guid brandId, string description)
            : base(brandId, typeof(Models.Brand))
        {
            BrandId = brandId;
            Description = description;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {BrandId} change description to {Description}";
        }
    }
}
