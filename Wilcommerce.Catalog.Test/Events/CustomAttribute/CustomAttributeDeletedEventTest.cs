using System;
using Wilcommerce.Catalog.Events.CustomAttribute;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.CustomAttribute
{
    public class CustomAttributeDeletedEventTest
    {
        [Fact]
        public void CustomAttributeDeletedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid attributeId = Guid.NewGuid();
            var @event = new CustomAttributeDeletedEvent(attributeId);

            Assert.Equal(attributeId, @event.AttributeId);

            Assert.Equal(attributeId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.CustomAttribute), @event.AggregateType);
        }
    }
}
