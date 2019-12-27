using System;
using Wilcommerce.Catalog.Events.Brand;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Brand
{
    public class BrandDeletedEventTest
    {
        [Fact]
        public void BrandDeletedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid brandId = Guid.NewGuid();

            var @event = new BrandDeletedEvent(brandId);

            Assert.Equal(brandId, @event.BrandId);
            Assert.Equal(brandId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Brand), @event.AggregateType);
        }
    }
}
