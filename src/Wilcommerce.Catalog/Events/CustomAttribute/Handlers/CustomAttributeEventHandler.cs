using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute.Handlers
{
    /// <summary>
    /// Handles the events related to Custom attributes
    /// </summary>
    public class CustomAttributeEventHandler :
        IHandleEvent<CustomAttributeCreatedEvent>,
        IHandleEvent<CustomAttributeValueAddedEvent>,
        IHandleEvent<CustomAttributeValueRemovedEvent>,
        IHandleEvent<CustomAttributeNameChangedEvent>,
        IHandleEvent<CustomAttributeDescriptionChangedEvent>,
        IHandleEvent<CustomAttributeUnitOfMeasureSetEvent>,
        IHandleEvent<CustomAttributeDataTypeChangedEvent>,
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
            EventStore = eventStore;
        }

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        public void Handle(CustomAttributeValueAddedEvent @event)
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
        public void Handle(CustomAttributeValueRemovedEvent @event)
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
        public void Handle(CustomAttributeNameChangedEvent @event)
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
        public void Handle(CustomAttributeDescriptionChangedEvent @event)
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
        public void Handle(CustomAttributeUnitOfMeasureSetEvent @event)
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
        public void Handle(CustomAttributeDataTypeChangedEvent @event)
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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
