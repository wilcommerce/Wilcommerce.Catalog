using System;
using Wilcommerce.Core.Common.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    /// <summary>
    /// Brand info updated
    /// </summary>
    public class BrandInfoUpdatedEvent : DomainEvent
    {
        /// <summary>
        /// Get the brand id
        /// </summary>
        public Guid BrandId { get; private set; }

        /// <summary>
        /// Get the brand name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get the brand url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Get the brand description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Get the brand logo
        /// </summary>
        public Image Logo { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="brandId">The brand id</param>
        /// <param name="name">The brand name</param>
        /// <param name="url">The brand url</param>
        /// <param name="description">The brand description</param>
        /// <param name="logo">The brand logo</param>
        /// <param name="userId">The user's id</param>
        public BrandInfoUpdatedEvent(Guid brandId, string name, string url, string description, Image logo, string userId)
            : base(brandId, typeof(Models.Brand), userId)
        {
            BrandId = brandId;
            Name = name;
            Url = url;
            Description = description;
            Logo = logo;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Brand {BrandId} updated. Name: {Name}, Url: {Url}, Description: {Description}";
        }
    }
}
