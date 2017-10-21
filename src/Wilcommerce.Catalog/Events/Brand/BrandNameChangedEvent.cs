using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    /// <summary>
    /// Brand name changed
    /// </summary>
    public class BrandNameChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the brand id
        /// </summary>
        public Guid BrandId { get; private set; }

        /// <summary>
        /// Get the brand name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="brandId">The brand id</param>
        /// <param name="name">The brand name</param>
        public BrandNameChangedEvent(Guid brandId, string name)
            : base(brandId, typeof(Models.Brand))
        {
            BrandId = brandId;
            Name = name;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Brand {BrandId} name changed to {Name}";
        }
    }
}
