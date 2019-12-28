using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Catalog.ReadModels;
using Xunit;

namespace Wilcommerce.Catalog.Test.ReadModels
{
    public class BrandExtensionsTest
    {
        #region Active tests
        [Fact]
        public void Active_Should_Throw_ArgumentNullException_If_Brands_Is_Null()
        {
            IQueryable<Brand> brands = null;

            var ex = Assert.Throws<ArgumentNullException>(() => BrandExtensions.Active(brands));
            Assert.Equal(nameof(brands), ex.ParamName);
        }

        [Fact]
        public void Active_Should_Return_Only_Brands_Not_Deleted()
        {
            var brand1 = Brand.Create("b1", "b1");
            var brand2 = Brand.Create("b2", "b2");
            var brand3 = Brand.Create("b3", "b3");
            brand3.Delete();

            IQueryable<Brand> brands = new Brand[]
            {
                brand1, brand2, brand3
            }.AsQueryable();

            var activeBrands = BrandExtensions.Active(brands).ToArray();
            Assert.True(activeBrands.All(b => !b.Deleted));
        }
        #endregion
    }
}
