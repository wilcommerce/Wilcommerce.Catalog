using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand.Handlers
{
    public class BrandEventHandler : 
        IHandleEvent<BrandCreatedEvent>,
        IHandleEvent<BrandNameChangedEvent>,
        IHandleEvent<BrandUrlChangedEvent>,
        IHandleEvent<BrandDescriptionChangedEvent>,
        IHandleEvent<BrandDeletedEvent>,
        IHandleEvent<BrandRestoredEvent>
    {
        public IEventStore EventStore { get; }

        public BrandEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
        }

        public void Handle(BrandCreatedEvent @event)
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

        public void Handle(BrandNameChangedEvent @event)
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

        public void Handle(BrandUrlChangedEvent @event)
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

        public void Handle(BrandDescriptionChangedEvent @event)
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

        public void Handle(BrandDeletedEvent @event)
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

        public void Handle(BrandRestoredEvent @event)
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
