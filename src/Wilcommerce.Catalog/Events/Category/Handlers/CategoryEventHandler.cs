using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category.Handlers
{
    /// <summary>
    /// Handles the events related to Category
    /// </summary>
    public class CategoryEventHandler :
        IHandleEvent<CategoryCreatedEvent>,
        IHandleEvent<CategoryInfoUpdatedEvent>,
        IHandleEvent<CategoryChildAddedEvent>,
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
            EventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
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

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(CategoryInfoUpdatedEvent @event)
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

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
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

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
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

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
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

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
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
