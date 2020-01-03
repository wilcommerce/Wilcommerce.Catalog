using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Catalog.ReadModels;
using Xunit;

namespace Wilcommerce.Catalog.Test.ReadModels
{
    public class ProductCategoryExtensionsTest
    {
        #region ProductsByCategory tests
        [Fact]
        public void ProductsByCategory_Should_Throw_ArgumentNullException_If_ProductCategories_Is_Null()
        {
            IQueryable<ProductCategory> productCategories = null;
            Guid categoryId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() => ProductCategoryExtensions.ProductsByCategory(productCategories, categoryId));
            Assert.Equal(nameof(productCategories), ex.ParamName);
        }

        [Fact]
        public void ProductsByCategory_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            IQueryable<ProductCategory> productCategories = new ProductCategory[]
            {
                new ProductCategory { ProductId = Guid.NewGuid(), CategoryId = Guid.NewGuid() },
                new ProductCategory { ProductId = Guid.NewGuid(), CategoryId = Guid.NewGuid() },
                new ProductCategory { ProductId = Guid.NewGuid(), CategoryId = Guid.NewGuid() }
            }.AsQueryable();
            Guid categoryId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => ProductCategoryExtensions.ProductsByCategory(productCategories, categoryId));
            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public void ProductsByCategory_Should_Return_Only_ProductCategories_With_The_Specified_Category()
        {
            Guid categoryId = Guid.NewGuid();

            IQueryable<ProductCategory> productCategories = new ProductCategory[]
            {
                new ProductCategory { ProductId = Guid.NewGuid(), CategoryId = categoryId },
                new ProductCategory { ProductId = Guid.NewGuid(), CategoryId = categoryId },
                new ProductCategory { ProductId = Guid.NewGuid(), CategoryId = Guid.NewGuid() }
            }.AsQueryable();

            var productCategoriesByCategory = ProductCategoryExtensions.ProductsByCategory(productCategories, categoryId).ToArray();
            Assert.True(productCategoriesByCategory.Length == 2);
        }
        #endregion

        #region Mains tests
        [Fact]
        public void Mains_Should_Throw_ArgumentNullException_If_ProductCategories_Is_Null()
        {
            IQueryable<ProductCategory> productCategories = null;

            var ex = Assert.Throws<ArgumentNullException>(() => ProductCategoryExtensions.Mains(productCategories));
            Assert.Equal(nameof(productCategories), ex.ParamName);
        }

        [Fact]
        public void Mains_Should_Return_Only_Categories_Set_As_Main_Category()
        {
            IQueryable<ProductCategory> productCategories = new ProductCategory[]
            {
                new ProductCategory { CategoryId = Guid.NewGuid(), ProductId = Guid.NewGuid(), IsMain = true },
                new ProductCategory { CategoryId = Guid.NewGuid(), ProductId = Guid.NewGuid(), IsMain = true },
                new ProductCategory { CategoryId = Guid.NewGuid(), ProductId = Guid.NewGuid(), IsMain = false }
            }.AsQueryable();

            var mainCategories = ProductCategoryExtensions.Mains(productCategories).ToArray();
            Assert.True(mainCategories.All(c => c.IsMain));
        }
        #endregion
    }
}
