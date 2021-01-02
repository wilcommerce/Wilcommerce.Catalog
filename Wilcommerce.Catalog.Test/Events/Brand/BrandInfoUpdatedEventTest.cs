using System;
using Wilcommerce.Catalog.Events.Brand;
using Wilcommerce.Core.Common.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Brand
{
    public class BrandInfoUpdatedEventTest
    {
        [Fact]
        public void BrandInfoUpdatedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid brandId = Guid.NewGuid();
            string name = "brand";
            string url = "brand";
            string description = "description";
            Image logo = new Image { MimeType = "image/jpg", Data = new byte[0] };
            string userId = Guid.NewGuid().ToString();

            var @event = new BrandInfoUpdatedEvent(brandId, name, url, description, logo, userId);

            Assert.Equal(brandId, @event.BrandId);
            Assert.Equal(name, @event.Name);
            Assert.Equal(url, @event.Url);
            Assert.Equal(description, @event.Description);
            Assert.Equal(logo, @event.Logo);

            Assert.Equal(brandId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Brand), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
