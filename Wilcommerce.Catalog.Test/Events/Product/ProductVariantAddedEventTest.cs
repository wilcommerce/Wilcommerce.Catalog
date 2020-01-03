using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductVariantAddedEventTest
    {
        [Fact]
        public void ProductVariantAddedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            string name = "variant";
            string ean = "ean";
            string sku = "sku";

            var @event = new ProductVariantAddedEvent(productId, name, ean, sku);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(name, @event.VariantName);
            Assert.Equal(ean, @event.EanCode);
            Assert.Equal(sku, @event.Sku);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
