using System;
using Wilcommerce.Catalog.Events.Category;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Category
{
    public class CategoryChildAddedEventTest
    {
        [Fact]
        public void CategoryChildAddedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid categoryId = Guid.NewGuid();
            Guid childId = Guid.NewGuid();

            var @event = new CategoryChildAddedEvent(categoryId, childId);

            Assert.Equal(categoryId, @event.CategoryId);
            Assert.Equal(childId, @event.ChildId);

            Assert.Equal(categoryId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Category), @event.AggregateType);
        }
    }
}
