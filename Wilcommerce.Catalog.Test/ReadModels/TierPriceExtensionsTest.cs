using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Catalog.ReadModels;
using Xunit;

namespace Wilcommerce.Catalog.Test.ReadModels
{
    public class TierPriceExtensionsTest
    {
        #region ByProduct tests
        [Fact]
        public void ByProduct_Should_Throw_ArgumentNullException_If_TierPrices_Is_Null()
        {
            IQueryable<TierPrice> tierPrices = null;
            Guid productId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() => TierPriceExtensions.ByProduct(tierPrices, productId));
            Assert.Equal(nameof(tierPrices), ex.ParamName);
        }

        [Fact]
        public void ByProduct_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            IQueryable<TierPrice> tierPrices = new TierPrice[0].AsQueryable();
            Guid productId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => TierPriceExtensions.ByProduct(tierPrices, productId));
            Assert.Equal(nameof(productId), ex.ParamName);
        }
        #endregion
    }
}
