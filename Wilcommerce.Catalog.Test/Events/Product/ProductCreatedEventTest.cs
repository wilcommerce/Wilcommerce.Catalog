using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductCreatedEventTest
    {
        [Fact]
        public void ProductCreatedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            string ean = "ean";
            string sku = "sku";
            string name = "name";
            string userId = Guid.NewGuid().ToString();

            var @event = new ProductCreatedEvent(productId, ean, sku, name, userId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(ean, @event.EanCode);
            Assert.Equal(sku, @event.Sku);
            Assert.Equal(name, @event.Name);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
