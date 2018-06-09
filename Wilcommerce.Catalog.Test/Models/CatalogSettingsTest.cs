using System;
using Wilcommerce.Catalog.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Models
{
    public class CatalogSettingsTest
    {
        [Fact]
        public void CatalogSettingsFactory_Should_Throw_ArgumentException_If_CategoriesPerPage_IsLessThan_Zero()
        {
            var ex = Assert.Throws<ArgumentException>(() => CatalogSettings.Create(
                -1,
                10,
                CatalogSettings.ViewType.LIST,
                CatalogSettings.ViewType.LIST
                ));

            Assert.Equal("categoriesPerPage", ex.ParamName);
        }

        [Fact]
        public void CatalogSettingsFactory_Should_Throw_ArgumentException_If_ProductsPerPage_IsLessThan_Zero()
        {
            var ex = Assert.Throws<ArgumentException>(() => CatalogSettings.Create(
                10,
                -1,
                CatalogSettings.ViewType.LIST,
                CatalogSettings.ViewType.LIST
                ));

            Assert.Equal("productsPerPage", ex.ParamName);
        }

        [Fact]
        public void SetCategoriesPerPage_Should_Throw_ArgumentException_If_CategoriesPerPage_IsLessThan_Zero()
        {
            var settings = CatalogSettings.Create(
                10,
                10,
                CatalogSettings.ViewType.LIST,
                CatalogSettings.ViewType.LIST
                );

            var ex = Assert.Throws<ArgumentException>(() => settings.SetCategoriesPerPage(-1));
            Assert.Equal("categoriesPerPage", ex.ParamName);
        }

        [Fact]
        public void SetProductsPerPage_Should_Throw_ArgumentException_If_ProductsPerPage_IsLessThan_Zero()
        {
            var settings = CatalogSettings.Create(
                10,
                10,
                CatalogSettings.ViewType.LIST,
                CatalogSettings.ViewType.LIST
                );

            var ex = Assert.Throws<ArgumentException>(() => settings.SetProductsPerPage(-1));
            Assert.Equal("productsPerPage", ex.ParamName);
        }

        [Fact]
        public void SetProductReviewsPerPage_Should_Throw_ArgumentException_If_ReviewsPerPage_IsLessThan_Zero()
        {
            var settings = CatalogSettings.Create(
                10,
                10,
                CatalogSettings.ViewType.LIST,
                CatalogSettings.ViewType.LIST
                );

            settings.AllowProductReviews(true);

            var ex = Assert.Throws<ArgumentException>(() => settings.SetProductReviewsPerPage(-1));
            Assert.Equal("reviewsPerPage", ex.ParamName);
        }

        [Fact]
        public void SetProductReviewsPerPage_Should_Throw_InvalidOperationException_If_ProductReviews_AreNot_Allowed()
        {
            var settings = CatalogSettings.Create(
                10,
                10,
                CatalogSettings.ViewType.LIST,
                CatalogSettings.ViewType.LIST
                );

            var ex = Assert.Throws<InvalidOperationException>(() => settings.SetProductReviewsPerPage(10));
            Assert.Equal("Reviews not allowed", ex.Message);
        }
    }
}
