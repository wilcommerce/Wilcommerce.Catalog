using System;
using Wilcommerce.Catalog.Events.Brand;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Brand
{
    public class BrandCreatedEventTest
    {
        [Fact]
        public void BrandCreatedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid brandId = Guid.NewGuid();
            string name = "brand";

            var @event = new BrandCreatedEvent(brandId, name);

            Assert.Equal(brandId, @event.BrandId);
            Assert.Equal(name, @event.Name);

            Assert.Equal(brandId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Brand), @event.AggregateType);
        }
    }
}
