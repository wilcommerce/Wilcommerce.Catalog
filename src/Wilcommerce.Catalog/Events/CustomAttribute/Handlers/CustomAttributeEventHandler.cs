using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute.Handlers
{
    /// <summary>
    /// Handles the events related to Custom attributes
    /// </summary>
    public class CustomAttributeEventHandler :
        IHandleEvent<CustomAttributeCreatedEvent>,
        IHandleEvent<CustomAttributeUpdatedEvent>,
        IHandleEvent<CustomAttributeDeletedEvent>,
        IHandleEvent<CustomAttributeRestoredEvent>
    {
        /// <summary>
        /// Get the event store
        /// </summary>
        public IEventStore EventStore { get; }

        /// <summary>
        /// Construct the event handler
        /// </summary>
        /// <param name="eventStore">The event store</param>
        public CustomAttributeEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(CustomAttributeCreatedEvent @event)
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
        public void Handle(CustomAttributeUpdatedEvent @event)
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
        public void Handle(CustomAttributeDeletedEvent @event)
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
        public void Handle(CustomAttributeRestoredEvent @event)
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
