using System;
using Wilcommerce.Catalog.Events.Product;
using Wilcommerce.Core.Common.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductTierPriceChangedEventTest
    {
        [Fact]
        public void ProductTierPriceChangedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid tierPriceId = Guid.NewGuid();
            int fromQuantity = 1;
            int toQuantity = 5;
            Currency price = new Currency { Amount = 10, Code = "EUR" };
            string userId = Guid.NewGuid().ToString();

            var @event = new ProductTierPriceChangedEvent(productId, tierPriceId, fromQuantity, toQuantity, price, userId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(tierPriceId, @event.TierPriceId);
            Assert.Equal(fromQuantity, @event.FromQuantity);
            Assert.Equal(toQuantity, @event.ToQuantity);
            Assert.Equal(price, @event.Price);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
