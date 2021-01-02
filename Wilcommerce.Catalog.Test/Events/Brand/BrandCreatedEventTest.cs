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
            string userId = Guid.NewGuid().ToString();

            var @event = new BrandCreatedEvent(brandId, name, userId);

            Assert.Equal(brandId, @event.BrandId);
            Assert.Equal(name, @event.Name);

            Assert.Equal(brandId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Brand), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
