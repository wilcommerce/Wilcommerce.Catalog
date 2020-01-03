using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductAttributeAddedEventTest
    {
        [Fact]
        public void ProductAttributeAddedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid attributeId = Guid.NewGuid();
            object value = "123";

            var @event = new ProductAttributeAddedEvent(productId, attributeId, value);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(attributeId, @event.AttributeId);
            Assert.Equal(value, @event.Value);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
