using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductImageAddedEventTest
    {
        [Fact]
        public void ProductImageAddedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            string name = "image";
            string originalName = "original";

            var @event = new ProductImageAddedEvent(productId, name, originalName);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(name, @event.Name);
            Assert.Equal(originalName, @event.OriginalName);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
