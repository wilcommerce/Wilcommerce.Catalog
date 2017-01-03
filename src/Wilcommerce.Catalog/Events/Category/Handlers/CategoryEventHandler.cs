using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category.Handlers
{
    public class CategoryEventHandler :
        IHandleEvent<CategoryCreatedEvent>,
        IHandleEvent<CategoryChildAddedEvent>,
        IHandleEvent<CategoryNameChangedEvent>,
        IHandleEvent<CategoryCodeChangedEvent>,
        IHandleEvent<CategoryDescriptionChangedEvent>,
        IHandleEvent<CategoryUrlChangedEvent>,
        IHandleEvent<CategoryDeletedEvent>,
        IHandleEvent<CategoryRestoredEvent>,
        IHandleEvent<CategoryChildRemovedEvent>
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

        public void Handle(CategoryChildAddedEvent @event)
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

        public void Handle(CategoryNameChangedEvent @event)
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

        public void Handle(CategoryCodeChangedEvent @event)
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

        public void Handle(CategoryDescriptionChangedEvent @event)
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

        public void Handle(CategoryUrlChangedEvent @event)
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

        public void Handle(CategoryDeletedEvent @event)
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

        public void Handle(CategoryRestoredEvent @event)
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

        public void Handle(CategoryChildRemovedEvent @event)
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
