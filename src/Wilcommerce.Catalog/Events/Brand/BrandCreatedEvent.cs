using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    /// <summary>
    /// New Brand created
    /// </summary>
    public class BrandCreatedEvent : DomainEvent
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
        /// <param name="userId">The user's id</param>
        public BrandCreatedEvent(Guid brandId, string name, string userId)
            : base(brandId, typeof(Models.Brand), userId)
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
            return $"Brand {Name} [{BrandId}] created successfully";
        }
    }
}
