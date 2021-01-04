using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductBrandSetEventTest
    {
        [Fact]
        public void ProductBrandSetEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid brandId = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();

            var @event = new ProductBrandSetEvent(productId, brandId, userId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(brandId, @event.BrandId);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
