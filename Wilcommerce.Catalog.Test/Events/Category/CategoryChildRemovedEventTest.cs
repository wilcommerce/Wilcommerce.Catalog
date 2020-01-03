using System;
using Wilcommerce.Catalog.Events.Category;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Category
{
    public class CategoryChildRemovedEventTest
    {
        [Fact]
        public void CategoryChildRemovedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid categoryId = Guid.NewGuid();
            Guid childId = Guid.NewGuid();

            var @event = new CategoryChildRemovedEvent(categoryId, childId);

            Assert.Equal(categoryId, @event.CategoryId);
            Assert.Equal(childId, @event.ChildId);

            Assert.Equal(categoryId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Category), @event.AggregateType);
        }
    }
}
