using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Core.Common.Domain.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Models
{
    public class ProductTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ProductFactory_Should_Throw_ArgumentNullException_If_Ean_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Product.Create(
                value,
                "sku",
                "product",
                "my-product"
                ));

            Assert.Equal("ean", ex.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ProductFactory_Should_Throw_ArgumentNullException_If_Sku_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Product.Create(
                "ean",
                value,
                "product",
                "my-product"
                ));

            Assert.Equal("sku", ex.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ProductFactory_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Product.Create(
                "ean",
                "sku",
                value,
                "my-product"
                ));

            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ProductFactory_Should_Throw_ArgumentNullException_If_Url_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Product.Create(
                "ean",
                "sku",
                "product",
                value
                ));

            Assert.Equal("url", ex.ParamName);
        }

        [Fact]
        public void Product_SetOnSale_Throws_ArgumentException_If_OnSaleFrom_Is_Greater_Than_OnSaleTo()
        {
            var onSaleFrom = DateTime.Now.AddDays(1);
            var onSaleTo = DateTime.Now;

            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentException>(() => product.SetOnSale(onSaleFrom, onSaleTo));
            Assert.Equal("The sale's start date must be precedent to the end date", ex.Message);
        }

        [Fact]
        public void Product_SetOnSale_Set_SaleDate_And_IsOnSale_To_True()
        {
            var onSaleFrom = DateTime.Now;
            var onSaleTo = DateTime.Now.AddDays(1);

            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.SetOnSale(onSaleFrom, onSaleTo);

            Assert.Equal(true, product.IsOnSale);
            Assert.Equal(onSaleFrom, product.OnSaleFrom);
            Assert.Equal(onSaleTo, product.OnSaleTo);
        }

        [Fact]
        public void Product_RemoveFromSale_Set_OnSaleTo_To_Now_And_IsOnSale_To_False()
        {
            var endSale = DateTime.Now;

            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.RemoveFromSale(endSale);

            Assert.Equal(false, product.IsOnSale);
            Assert.Equal(endSale, product.OnSaleTo);
        }

        [Fact]
        public void Product_AddCategory_Throws_ArgumentNullException_If_Category_IsNull()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.AddCategory(null));
            Assert.Equal("category", ex.ParamName);
        }

        [Fact]
        public void Product_AddCategory_Throws_ArgumentException_If_Category_Is_Already_In_Collection()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var category = Category.Create("CAT01", "Category", "category");
            product.AddCategory(category);

            var ex = Assert.Throws<ArgumentException>(() => product.AddCategory(category));
            Assert.Equal("The category is already in collection", ex.Message);
        }

        [Fact]
        public void Product_AddCategory_Should_Increment_Categories_Number()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            int categoryNumber = product.Categories.Count();

            var category = Category.Create("CAT01", "Category", "category");
            product.AddCategory(category);

            Assert.Equal(categoryNumber + 1, product.Categories.Count());
        }

        [Fact]
        public void Product_AddVariant_Throws_ArgumentNullException_If_Product_IsNull()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.AddVariant(null));
            Assert.Equal("product", ex.ParamName);
        }

        [Fact]
        public void Product_AddVariant_Throws_ArgumentException_If_Product_Is_Already_In_Collection()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var product2 = Product.Create("ean2", "sku2", "product2", "my-product-2");
            product.AddVariant(product2);

            var ex = Assert.Throws<ArgumentException>(() => product.AddVariant(product2));
            Assert.Equal("The product is already in collection", ex.Message);
        }

        [Fact]
        public void Product_AddVariant_Should_Increment_Products_Number()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            int variantNumber = product.Variants.Count();

            var product2 = Product.Create("ean2", "sku2", "product2", "my-product-2");
            product.AddVariant(product2);

            Assert.Equal(variantNumber + 1, product.Variants.Count());
        }

        [Fact]
        public void Product_AddTierPrice_Throws_InvalidOperationException_If_TierPrices_Are_Disabled()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.AddTierPrice(1, 10, new Currency { Code = "EUR", Amount = 10 }));
            Assert.Equal("Tier prices not enabled", ex.Message);
        }

        [Fact]
        public void Product_AddTierPrice_Throws_ArgumentNullException_If_Price_IsNull()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.EnableTierPrices();

            var ex = Assert.Throws<ArgumentNullException>(() => product.AddTierPrice(1, 10, null));
            Assert.Equal("price", ex.ParamName);
        }

        [Fact]
        public void Product_AddTierPrice_Throws_ArgumentException_If_TierPrice_Is_Already_In_Collection()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.EnableTierPrices();

            int fromQuantity = 1;
            int toQuantity = 10;
            product.AddTierPrice(fromQuantity, toQuantity, new Currency { Code = "EUR", Amount = 10 });

            var ex = Assert.Throws<ArgumentException>(() => product.AddTierPrice(fromQuantity, toQuantity, new Currency { Code = "EUR", Amount = 10 }));
            Assert.Equal("The tier price is already in collection", ex.Message);
        }

        [Fact]
        public void Product_AddTierPrice_Should_Increment_TierPrice_Number()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.EnableTierPrices();

            int tierPrices = product.TierPrices.Count();

            int fromQuantity = 1;
            int toQuantity = 10;
            product.AddTierPrice(fromQuantity, toQuantity, new Currency { Code = "EUR", Amount = 10 });

            Assert.Equal(tierPrices + 1, product.TierPrices.Count());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Product_AddReview_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.AddReview(value, 2));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void Product_AddReview_Should_Throw_ArgumentException_If_Rating_Is_LessThan_Zero()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentException>(() => product.AddReview("user", -1));
            Assert.Equal("rating", ex.ParamName);
        }

        [Fact]
        public void Product_AddReview_Should_Increment_Rating_Number()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            int reviewCount = product.Reviews.Count();
            product.AddReview("user", 2);

            Assert.Equal(reviewCount + 1, product.Reviews.Count());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Product_AddImage_Should_Throw_ArgumentNullException_If_Path_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.AddImage(value, "name", "original", true, DateTime.Now));
            Assert.Equal("path", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Product_AddImage_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.AddImage("path/to/image.ext", value, "original", true, DateTime.Now));
            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Product_AddImage_Should_Throw_ArgumentNullException_If_OriginalName_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.AddImage("path/to/image.ext", "name", value, true, DateTime.Now));
            Assert.Equal("originalName", ex.ParamName);
        }

        [Fact]
        public void Product_AddImage_Should_Increment_Image_Number()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            int imageCount = product.Images.Count();
            product.AddImage("path/to/image.ext", "name", "orignal_name.ext", true, DateTime.Now);

            Assert.Equal(imageCount + 1, product.Images.Count());
        }
    }
}
