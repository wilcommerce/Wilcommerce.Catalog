using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductAttributeRemovedEventTest
    {
        [Fact]
        public void ProductAttributeRemovedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid attributeId = Guid.NewGuid();

            var @event = new ProductAttributeRemovedEvent(productId, attributeId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(attributeId, @event.AttributeId);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
