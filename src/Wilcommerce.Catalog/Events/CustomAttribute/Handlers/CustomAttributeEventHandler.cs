using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute.Handlers
{
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
        public IEventStore EventStore { get; }

        public CustomAttributeEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
        }

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
