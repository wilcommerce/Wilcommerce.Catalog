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
        public void Delete_Should_Throw_InvalidOperationException_If_Product_IsDeleted()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.Delete();

            var ex = Assert.Throws<InvalidOperationException>(() => product.Delete());
            Assert.Equal("Product already deleted", ex.Message);
        }

        [Fact]
        public void Restore_Should_Throw_InvalidOperationException_If_Product_IsNotDeleted()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.Restore());
            Assert.Equal("Product is not deleted", ex.Message);
        }

        [Fact]
        public void SetUnitInStock_Should_Throw_ArgumentException_If_Unit_IsLessThan_Zero()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentException>(() => product.SetUnitInStock(-1));
            Assert.Equal("Stock unit cannot be less than zero", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeEanCode_Should_Throw_ArgumentNullException_If_EanCode_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.ChangeEanCode(value));
            Assert.Equal("ean", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeSku_Should_Throw_ArgumentNullException_If_Sku_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.ChangeSku(value));
            Assert.Equal("sku", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeName_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.ChangeName(value));
            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeDescription_Should_Throw_ArgumentNullException_If_Description_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.ChangeDescription(value));
            Assert.Equal("description", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeUrl_Should_Throw_ArgumentNullException_If_Url_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.ChangeUrl(value));
            Assert.Equal("url", ex.ParamName);
        }

        [Fact]
        public void SetPrice_Should_Throw_ArgumentNullException_If_Price_IsNull()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.SetPrice(null));
            Assert.Equal("price", ex.ParamName);
        }

        [Fact]
        public void SetPrice_Should_Throw_ArgumentException_If_Price_Amount_IsLessThan_Zero()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var price = new Currency
            {
                Code = "EUR",
                Amount = -10
            };

            var ex = Assert.Throws<ArgumentException>(() => product.SetPrice(price));
            Assert.Equal("Price amount cannot be less than zero", ex.Message);
        }

        [Fact]
        public void EnableTierPrice_Should_Throw_InvalidOperationException_If_TierPrices_Are_Enabled()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );
            product.EnableTierPrices();

            var ex = Assert.Throws<InvalidOperationException>(() => product.EnableTierPrices());
            Assert.Equal("Tier prices already enabled", ex.Message);
        }

        [Fact]
        public void DisableTierPrice_Should_Throw_InvalidOperationException_If_TierPrices_Are_Disabled()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.DisableTierPrices());
            Assert.Equal("Tier prices already disabled", ex.Message);
        }

        [Fact]
        public void SetVendor_Should_Throw_ArgumentNullException_If_Vendor_IsNull()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.SetVendor(null));
            Assert.Equal("vendor", ex.ParamName);
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

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Product_AddVariant_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var price = new Currency { Code = "EUR", Amount = 10 };
            var ex = Assert.Throws<ArgumentNullException>(() => product.AddVariant(value, "ean", "sku", price));

            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Product_AddVariant_Should_Throw_ArgumentNullException_If_Ean_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var price = new Currency { Code = "EUR", Amount = 10 };
            var ex = Assert.Throws<ArgumentNullException>(() => product.AddVariant("name", value, "sku", price));

            Assert.Equal("ean", ex.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Product_AddVariant_Should_Throw_ArgumentNullException_If_Sku_IsEmpty(string value)
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var price = new Currency { Code = "EUR", Amount = 10 };
            var ex = Assert.Throws<ArgumentNullException>(() => product.AddVariant("name", "ean", value, price));

            Assert.Equal("sku", ex.ParamName);
        }

        [Fact]
        public void Product_AddVariant_Should_Throw_ArgumentNullException_If_Price_IsNull()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.AddVariant("name", "ean", "sku", null));
            Assert.Equal("price", ex.ParamName);
        }

        [Fact]
        public void Product_AddVariant_Should_Throw_ArgumentException_If_PriceAmount_IsLessThanZero()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var price = new Currency { Code = "EUR", Amount = -10 };
            var ex = Assert.Throws<ArgumentException>(() => product.AddVariant("name", "ean", "sku", price));

            Assert.Equal("Price amount cannot be less than zero", ex.Message);
        }

        [Fact]
        public void Product_AddVariant_Should_Throw_InvalidOperationException_If_Variant_Is_Already_InCollection()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var price = new Currency { Code = "EUR", Amount = 10 };
            product.AddVariant("name", "ean", "sku", price);
            var ex = Assert.Throws<InvalidOperationException>(() => product.AddVariant("name", "ean", "sku", price));

            Assert.Equal("The variant is already in collection", ex.Message);
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

            product.AddVariant("product2", "ean2", "sku2", new Currency { Code = "EUR", Amount = 10 });
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

        [Fact]
        public void ApproveReview_Should_Throw_InvalidOperationException_If_Review_DoesNot_Exist()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.ApproveReview(Guid.NewGuid()));
            Assert.Equal("Review not found", ex.Message);
        }

        [Fact]
        public void ApproveReview_Should_Set_Approved_To_True()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.AddReview("review", 2);
            var review = product.Reviews.First();

            product.ApproveReview(review.Id);

            var rev = product.Reviews.First(r => r.Id == review.Id);
            Assert.Equal(true, rev.Approved);
        }

        [Fact]
        public void RemoveReviewApproval_Should_Throw_InvalidOperationException_If_Review_DoesNot_Exist()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.RemoveReviewApproval(Guid.NewGuid()));
            Assert.Equal("Review not found", ex.Message);
        }

        [Fact]
        public void RemoveReviewApproval_Should_Set_Approved_To_False()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.AddReview("review", 2);
            var review = product.Reviews.First();

            product.RemoveReviewApproval(review.Id);

            var rev = product.Reviews.First(r => r.Id == review.Id);
            Assert.Equal(false, rev.Approved);
        }

        [Fact]
        public void DeleteReview_Should_Throw_InvalidOperationException_If_Review_DoesNot_Exist()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.DeleteReview(Guid.NewGuid()));
            Assert.Equal("Review not found", ex.Message);
        }

        [Fact]
        public void DeleteReview_Should_Remove_Review()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.AddReview("review", 2);
            var review = product.Reviews.First();

            product.DeleteReview(review.Id);
            Assert.Equal(0, product.Reviews.Count(r => r.Id == review.Id));
        }

        [Fact]
        public void DeleteAttribute_Should_Throw_InvalidOperationException_If_Attribute_DoesNot_Exist()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.DeleteAttribute(Guid.NewGuid()));
            Assert.Equal("Attribute not found", ex.Message);
        }

        [Fact]
        public void DeleteAttribute_Should_Remove_Attribute()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.AddAttribute(CustomAttribute.Create("attribute", "number"), "value");
            var attribute = product.Attributes.First();

            product.DeleteAttribute(attribute.Id);
            Assert.Equal(0, product.Attributes.Count(a => a.Id == attribute.Id));
        }

        [Fact]
        public void DeleteImage_Should_Throw_InvalidOperationException_If_Image_DoesNot_Exist()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.DeleteImage(Guid.NewGuid()));
            Assert.Equal("Image not found", ex.Message);
        }

        [Fact]
        public void DeleteImage_Should_Remove_Image()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.AddImage("path", "name", "name", true, DateTime.Now);
            var image = product.Images.First();

            product.DeleteImage(image.Id);
            Assert.Equal(0, product.Images.Count(i => i.Id == image.Id));
        }

        [Fact]
        public void DeleteTierPrice_Should_Throw_InvalidOperationException_If_TierPrice_DoesNot_Exist()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.DeleteTierPrice(Guid.NewGuid()));
            Assert.Equal("Tier price not found", ex.Message);
        }

        [Fact]
        public void DeleteTierPrice_Should_Remove_TierPrice()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.EnableTierPrices();
            product.AddTierPrice(1, 10, new Currency
            {
                Code = "EUR",
                Amount = 10
            });

            var tp = product.TierPrices.First();
            product.DeleteTierPrice(tp.Id);

            Assert.Equal(0, product.TierPrices.Count(t => t.Id == tp.Id));
        }

        [Fact]
        public void RemoveVariant_Should_Throw_InvalidOperationException_If_Variant_DoesNot_Exist()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.RemoveVariant(Guid.NewGuid()));
            Assert.Equal("Variant not found", ex.Message);
        }

        [Fact]
        public void RemoveVariant_Should_Remove_Variant()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var price = new Currency
            {
                Code = "EUR",
                Amount = 10
            };

            product.AddVariant("name", "ean", "sku", price);
            var variant = product.Variants.First();

            product.RemoveVariant(variant.Id);
            Assert.Equal(0, product.Variants.Count(v => v.Id == variant.Id));
        }

        [Fact]
        public void SetSeoData_Should_Throw_ArgumentNullException_If_Seo_IsNull()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => product.SetSeoData(null));
            Assert.Equal("seo", ex.ParamName);
        }

        [Fact]
        public void ChangeTierPrice_Should_Throw_InvalidOperationException_If_TierPrice_Are_Disabled()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => product.ChangeTierPrice(Guid.NewGuid(), 1, 15, new Currency { Code = "EUR", Amount = 20 }));
            Assert.Equal("Tier price disabled", ex.Message);
        }

        [Fact]
        public void ChangeTierPrice_Should_Throw_InvalidOperationException_If_TierPrice_IsNull()
        {
            var product = Product.Create(
                "ean",
                "sku",
                "product",
                "my-product"
                );

            product.EnableTierPrices();

            var ex = Assert.Throws<InvalidOperationException>(() => product.ChangeTierPrice(Guid.NewGuid(), 1, 15, new Currency { Code = "EUR", Amount = 20 }));
            Assert.Equal("Tier price not found", ex.Message);
        }
    }
}
