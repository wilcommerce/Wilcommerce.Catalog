using System;
using Wilcommerce.Core.Common.Models;
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
        public Guid Id { get; protected set; }

        #region Constructor
        /// <summary>
        /// Construct the brand
        /// </summary>
        protected Brand()
        {
            Logo = new Image();
            Seo = new SeoData();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the brand name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Get or set a description for the brand
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Get or set the brand url (unique slug)
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Get or set whether the brand is deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Get or set the brand logo
        /// </summary>
        public virtual Image Logo { get; protected set; }

        /// <summary>
        /// Get or set the seo data for the brand
        /// </summary>
        public virtual SeoData Seo { get; protected set; }

        /// <summary>
        /// Get the creation date
        /// </summary>
        public DateTime CreationDate { get; protected set; }
        #endregion

        #region Behaviors
        /// <summary>
        /// Change the brand name
        /// </summary>
        /// <param name="name">The new brand name</param>
        public virtual void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Change the brand description
        /// </summary>
        /// <param name="description">The brand description</param>
        public virtual void ChangeDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Change the brand url
        /// </summary>
        /// <param name="url">The new brand url</param>
        public virtual void ChangeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            Url = url;
        }

        /// <summary>
        /// Set the brand logo
        /// </summary>
        /// <param name="logo">The logo image</param>
        public virtual void SetLogo(Image logo)
        {
            Logo = logo ?? throw new ArgumentNullException(nameof(logo));
        }

        /// <summary>
        /// Set the brand seo data
        /// </summary>
        /// <param name="seo">The seo data</param>
        public virtual void SetSeoData(SeoData seo)
        {
            Seo = seo ?? throw new ArgumentNullException(nameof(seo));
        }

        /// <summary>
        /// Set the brand as deleted
        /// </summary>
        public virtual void Delete()
        {
            if (Deleted)
            {
                throw new InvalidOperationException("The brand is already deleted");
            }

            Deleted = true;
        }

        /// <summary>
        /// Restore the deleted brand
        /// </summary>
        public virtual void Restore()
        {
            if (!Deleted)
            {
                throw new InvalidOperationException("The brand is not deleted");
            }

            Deleted = false;
        }
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
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                Name = name,
                Url = url,
                CreationDate = DateTime.Now
            };

            return brand;
        }

        #endregion
    }
}
