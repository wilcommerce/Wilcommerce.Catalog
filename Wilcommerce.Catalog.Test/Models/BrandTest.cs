using System;
using Wilcommerce.Catalog.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Models
{
    public class BrandTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void BrandFactory_Should_Throw_ArgumentException_If_Name_IsEmpty(string name)
        {
            var ex = Assert.Throws<ArgumentException>(() => Brand.Create(
                name,
                "my-brand"
                ));

            Assert.Equal(nameof(name), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void BrandFactory_Should_Throw_ArgumentException_If_Url_IsEmpty(string url)
        {
            var ex = Assert.Throws<ArgumentException>(() => Brand.Create(
                "My Brand",
                url
                ));

            Assert.Equal(nameof(url), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeName_Should_Throw_ArgumentException_If_Name_IsEmpty(string name)
        {
            var brand = Brand.Create("brand", "brand");

            var ex = Assert.Throws<ArgumentException>(() => brand.ChangeName(name));
            Assert.Equal(nameof(name), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeDescription_Should_Clear_If_Description_IsEmpty(string value)
        {
            var brand = Brand.Create("brand", "brand");
            brand.ChangeDescription(value);

            Assert.True(string.IsNullOrWhiteSpace(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeUrl_Should_Throw_ArgumentException_If_Url_IsEmpty(string url)
        {
            var brand = Brand.Create("brand", "brand");

            var ex = Assert.Throws<ArgumentException>(() => brand.ChangeUrl(url));
            Assert.Equal(nameof(url), ex.ParamName);
        }

        [Fact]
        public void SetLogo_Should_Throw_ArgumentNullException_If_Logo_IsNull()
        {
            var brand = Brand.Create("brand", "brand");

            var ex = Assert.Throws<ArgumentNullException>(() => brand.SetLogo(null));
            Assert.Equal("logo", ex.ParamName);
        }

        [Fact]
        public void Delete_Should_Throw_InvalidOperationException_If_Brand_IsDeleted()
        {
            var brand = Brand.Create("brand", "brand");
            brand.Delete();

            var ex = Assert.Throws<InvalidOperationException>(() => brand.Delete());
            Assert.Equal("The brand is already deleted", ex.Message);
        }

        [Fact]
        public void Restore_Should_Throw_InvalidOperationException_If_Brand_IsNotDeleted()
        {
            var brand = Brand.Create("brand", "brand");

            var ex = Assert.Throws<InvalidOperationException>(() => brand.Restore());
            Assert.Equal("The brand is not deleted", ex.Message);
        }

        [Fact]
        public void SetSeoData_Should_Throw_ArgumentNullException_If_Seo_IsNull()
        {
            var brand = Brand.Create("brand", "brand");

            var ex = Assert.Throws<ArgumentNullException>(() => brand.SetSeoData(null));
            Assert.Equal("seo", ex.ParamName);
        }
    }
}
