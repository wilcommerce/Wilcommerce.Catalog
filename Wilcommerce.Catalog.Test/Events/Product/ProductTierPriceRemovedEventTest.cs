using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductTierPriceRemovedEventTest
    {
        [Fact]
        public void ProductTierPriceRemovedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid tierPriceId = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();

            var @event = new ProductTierPriceRemovedEvent(productId, tierPriceId, userId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(tierPriceId, @event.TierPriceId);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
