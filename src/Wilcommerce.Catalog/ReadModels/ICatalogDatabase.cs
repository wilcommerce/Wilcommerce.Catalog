using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    public interface ICatalogDatabase
    {
        IQueryable<Brand> Brands { get; }

        IQueryable<Category> Categories { get; }

        IQueryable<CustomAttribute> CustomAttributes { get; }

        IQueryable<Product> Products { get; }

        IQueryable<ProductAttribute> ProductAttributes { get; }

        IQueryable<ProductImage> ProductImages { get; }

        IQueryable<ProductReview> ProductReviews { get; }

        IQueryable<TierPrice> TierPrices { get; }

        IQueryable<ProductCategory> ProductCategories { get; }

        IQueryable<CatalogSettings> Settings { get; }
    }
}
