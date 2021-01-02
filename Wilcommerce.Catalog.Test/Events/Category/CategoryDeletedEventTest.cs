using System;
using Wilcommerce.Catalog.Events.Category;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Category
{
    public class CategoryDeletedEventTest
    {
        [Fact]
        public void CategoryDeletedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid categoryId = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();
            var @event = new CategoryDeletedEvent(categoryId, userId);

            Assert.Equal(categoryId, @event.CategoryId);
            Assert.Equal(categoryId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Category), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
