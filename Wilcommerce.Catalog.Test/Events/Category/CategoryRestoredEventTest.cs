using System;
using Wilcommerce.Catalog.Events.Category;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Category
{
    public class CategoryRestoredEventTest
    {
        [Fact]
        public void CategoryRestoredEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid categoryId = Guid.NewGuid();
            var @event = new CategoryRestoredEvent(categoryId);

            Assert.Equal(categoryId, @event.CategoryId);
            Assert.Equal(categoryId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Category), @event.AggregateType);
        }
    }
}
