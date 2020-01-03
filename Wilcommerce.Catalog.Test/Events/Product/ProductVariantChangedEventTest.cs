using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductVariantChangedEventTest
    {
        [Fact]
        public void ProductVariantChangedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid variantId = Guid.NewGuid();
            string name = "name";
            string ean = "ean";
            string sku = "sku";

            var @event = new ProductVariantChangedEvent(productId, variantId, name, ean, sku);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(variantId, @event.VariantId);
            Assert.Equal(name, @event.VariantName);
            Assert.Equal(ean, @event.EanCode);
            Assert.Equal(sku, @event.Sku);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
