using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    /// <summary>
    /// Defines an readonly access point for the catalog entities
    /// </summary>
    public interface ICatalogDatabase
    {
        /// <summary>
        /// Get the brands
        /// </summary>
        IQueryable<Brand> Brands { get; }

        /// <summary>
        /// Get the categories
        /// </summary>
        IQueryable<Category> Categories { get; }

        /// <summary>
        /// Get the custom attributes
        /// </summary>
        IQueryable<CustomAttribute> CustomAttributes { get; }

        /// <summary>
        /// Get the products
        /// </summary>
        IQueryable<Product> Products { get; }
        
        /// <summary>
        /// Get the product attributes
        /// </summary>
        IQueryable<ProductAttribute> ProductAttributes { get; }

        /// <summary>
        /// Get the product images
        /// </summary>
        IQueryable<ProductImage> ProductImages { get; }

        /// <summary>
        /// Get the product reviews
        /// </summary>
        IQueryable<ProductReview> ProductReviews { get; }

        /// <summary>
        /// Get the product tier prices
        /// </summary>
        IQueryable<TierPrice> TierPrices { get; }

        /// <summary>
        /// Get the product categories
        /// </summary>
        IQueryable<ProductCategory> ProductCategories { get; }

        /// <summary>
        /// Get the catalog settings
        /// </summary>
        IQueryable<CatalogSettings> Settings { get; }
    }
}
