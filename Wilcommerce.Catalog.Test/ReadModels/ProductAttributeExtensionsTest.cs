using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Catalog.ReadModels;
using Xunit;

namespace Wilcommerce.Catalog.Test.ReadModels
{
    public class ProductAttributeExtensionsTest
    {
        #region ByProduct tests
        [Fact]
        public void ByProduct_Should_Throw_ArgumentNullException_If_Attributes_Is_Null()
        {
            IQueryable<ProductAttribute> attributes = null;
            Guid productId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() => ProductAttributeExtensions.ByProduct(attributes, productId));
            Assert.Equal(nameof(attributes), ex.ParamName);
        }

        [Fact]
        public void ByProduct_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            var product = Product.Create("ean", "sku", "name", "url");

            IQueryable<ProductAttribute> attributes = new ProductAttribute[]
            {
                new ProductAttribute{ Id = Guid.NewGuid(), Value = 123, Attribute = CustomAttribute.Create("a1", "t1"), Product = product },
                new ProductAttribute{ Id = Guid.NewGuid(), Value = 123, Attribute = CustomAttribute.Create("a2", "t2"), Product = product },
                new ProductAttribute{ Id = Guid.NewGuid(), Value = 123, Attribute = CustomAttribute.Create("a3", "t3"), Product = product }
            }.AsQueryable();
            Guid productId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => ProductAttributeExtensions.ByProduct(attributes, productId));
            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public void ByProduct_Should_Return_Product_Attributes_With_The_Specified_Product()
        {
            var product = Product.Create("ean", "sku", "name", "url");

            IQueryable<ProductAttribute> attributes = new ProductAttribute[]
            {
                new ProductAttribute{ Id = Guid.NewGuid(), Value = 123, Attribute = CustomAttribute.Create("a1", "t1"), Product = product },
                new ProductAttribute{ Id = Guid.NewGuid(), Value = 123, Attribute = CustomAttribute.Create("a2", "t2"), Product = product },
                new ProductAttribute{ Id = Guid.NewGuid(), Value = 123, Attribute = CustomAttribute.Create("a3", "t3"), Product = Product.Create("ean1", "sku1", "name1", "url1") }
            }.AsQueryable();
            Guid productId = product.Id;

            var attributesByProduct = ProductAttributeExtensions.ByProduct(attributes, productId).ToArray();
            Assert.True(attributesByProduct.All(a => a.Product.Id == productId));
        }
        #endregion
    }
}
