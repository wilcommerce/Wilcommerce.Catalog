using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductVariantRemovedEventTest
    {
        [Fact]
        public void ProductVariantRemovedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid variantId = Guid.NewGuid();

            var @event = new ProductVariantRemovedEvent(productId, variantId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(variantId, @event.VariantId);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
