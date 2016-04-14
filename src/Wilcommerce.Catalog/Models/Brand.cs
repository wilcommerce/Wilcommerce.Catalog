using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents a brand
    /// </summary>
    public class Brand : IAggregateRoot
    {
        /// <summary>
        /// Get or set the brand id
        /// </summary>
        public Guid Id { get; set; }

        #region Constructor
        protected Brand() { }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the brand name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set a description for the brand
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get or set the brand url (unique slug)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Get or set whether the brand is deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Get or set the brand logo
        /// </summary>
        public Image Logo { get; set; }
        
        #endregion

        #region Factory Methods
        /// <summary>
        /// Create a new Brand
        /// </summary>
        /// <param name="name">The brand name</param>
        /// <param name="url">The brand url</param>
        /// <returns>The brand created</returns>
        public static Brand Create(string name, string url)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                Name = name,
                Url = url
            };

            return brand;
        }

        #endregion
    }
}
