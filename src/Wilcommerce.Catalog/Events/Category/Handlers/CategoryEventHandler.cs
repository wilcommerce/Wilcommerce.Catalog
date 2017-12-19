using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category.Handlers
{
    /// <summary>
    /// Handles the events related to Category
    /// </summary>
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
        /// <summary>
        /// Get the event store
        /// </summary>
        public IEventStore EventStore { get; }

        /// <summary>
        /// Construct the event handler
        /// </summary>
        /// <param name="eventStore">The event store</param>
        public CategoryEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
        }

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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
