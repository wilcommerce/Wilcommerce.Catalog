using System;
using Wilcommerce.Catalog.Events.CustomAttribute;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.CustomAttribute
{
    public class CustomAttributeCreatedEventTest
    {
        [Fact]
        public void CustomAttributeCreatedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid attributeId = Guid.NewGuid();
            string name = "attribute";
            string type = "string";

            var @event = new CustomAttributeCreatedEvent(attributeId, name, type);

            Assert.Equal(attributeId, @event.AttributeId);
            Assert.Equal(name, @event.Name);
            Assert.Equal(type, @event.Type);

            Assert.Equal(attributeId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.CustomAttribute), @event.AggregateType);
        }
    }
}
