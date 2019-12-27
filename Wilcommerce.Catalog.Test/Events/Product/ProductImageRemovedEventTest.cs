using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductImageRemovedEventTest
    {
        [Fact]
        public void ProductImageRemovedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid imageId = Guid.NewGuid();

            var @event = new ProductImageRemovedEvent(productId, imageId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(imageId, @event.ImageId);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
