using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    /// <summary>
    /// Brand url changed
    /// </summary>
    public class BrandUrlChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the brand id
        /// </summary>
        public Guid BrandId { get; private set; }

        /// <summary>
        /// Get the brand url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="brandId">The brand id</param>
        /// <param name="url">The brand url</param>
        public BrandUrlChangedEvent(Guid brandId, string url)
            : base(brandId, typeof(Models.Brand))
        {
            BrandId = brandId;
            Url = url;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {BrandId} change url to {Url}";
        }
    }
}
