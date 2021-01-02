using System;
using Wilcommerce.Catalog.Events.CustomAttribute;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.CustomAttribute
{
    public class CustomAttributeRestoredEventTest
    {
        [Fact]
        public void CustomAttributeRestoredEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid attributeId = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();
            var @event = new CustomAttributeRestoredEvent(attributeId, userId);

            Assert.Equal(attributeId, @event.AttributeId);

            Assert.Equal(attributeId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.CustomAttribute), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
