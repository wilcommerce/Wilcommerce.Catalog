using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductDeletedEventTest
    {
        [Fact]
        public void ProductDeletedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();
            var @event = new ProductDeletedEvent(productId, userId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
