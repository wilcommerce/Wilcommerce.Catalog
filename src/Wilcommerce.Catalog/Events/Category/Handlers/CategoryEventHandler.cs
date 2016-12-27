using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category.Handlers
{
    public class CategoryEventHandler :
        IHandleEvent<CategoryCreatedEvent>,
        IHandleEvent<CategoryNotCreatedEvent>
    {
        public IEventStore EventStore { get; }

        public CategoryEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
        }

        public void Handle(CategoryCreatedEvent @event)
        {
            try
            {
                EventStore.Save(@event);
            }
            catch 
            {
                throw;
            }
        }

        public void Handle(CategoryNotCreatedEvent @event)
        {
            try
            {
                EventStore.Save(@event);
            }
            catch
            {
                throw;
            }
        }
    }
}
