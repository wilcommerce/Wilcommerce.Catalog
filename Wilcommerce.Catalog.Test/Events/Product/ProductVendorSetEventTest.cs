using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductVendorSetEventTest
    {
        [Fact]
        public void ProductVendorSetEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid vendorId = Guid.NewGuid();

            var @event = new ProductVendorSetEvent(productId, vendorId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(vendorId, @event.VendorId);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
