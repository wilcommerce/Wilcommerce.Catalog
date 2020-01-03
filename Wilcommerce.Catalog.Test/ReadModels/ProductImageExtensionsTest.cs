using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Catalog.ReadModels;
using Xunit;

namespace Wilcommerce.Catalog.Test.ReadModels
{
    public class ProductImageExtensionsTest
    {
        #region ByProduct tests
        [Fact]
        public void ByProduct_Should_Throws_ArgumentNullException_If_Images_Is_Null()
        {
            IQueryable<ProductImage> images = null;
            Guid productId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() => ProductImageExtensions.ByProduct(images, productId));
            Assert.Equal(nameof(images), ex.ParamName);
        }

        [Fact]
        public void ByProduct_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            IQueryable<ProductImage> images = new ProductImage[0].AsQueryable();
            Guid productId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => ProductImageExtensions.ByProduct(images, productId));
            Assert.Equal(nameof(productId), ex.ParamName);
        }
        #endregion
    }
}
