using System;
using System.Collections.Generic;
using Wilcommerce.Catalog.Events.CustomAttribute;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.CustomAttribute
{
    public class CustomAttributeUpdatedEventTest
    {
        [Fact]
        public void CustomAttributeUpdatedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid attributeId = Guid.NewGuid();
            string name = "name";
            string type = "string";
            string description = "description";
            string unitOfMeasure = "unit";
            IEnumerable<object> values = new[] { "v1", "v2" };

            var @event = new CustomAttributeUpdatedEvent(attributeId, name, type, description, unitOfMeasure, values);

            Assert.Equal(attributeId, @event.AttributeId);
            Assert.Equal(name, @event.Name);
            Assert.Equal(type, @event.Type);
            Assert.Equal(description, @event.Description);
            Assert.Equal(unitOfMeasure, @event.UnitOfMeasure);
            Assert.Equal(values, @event.Values);

            Assert.Equal(attributeId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.CustomAttribute), @event.AggregateType);
        }
    }
}
