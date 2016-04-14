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
        public void BrandFactory_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Brand.Create(
                value,
                "my-brand"
                ));

            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void BrandFactory_Should_Throw_ArgumentNullException_If_Url_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Brand.Create(
                "My Brand",
                value
                ));

            Assert.Equal("url", ex.ParamName);
        }
    }
}
