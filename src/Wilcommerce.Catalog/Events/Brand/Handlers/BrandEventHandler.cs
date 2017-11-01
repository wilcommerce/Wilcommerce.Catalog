using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand.Handlers
{
    /// <summary>
    /// Handles the events related to Brand
    /// </summary>
    public class BrandEventHandler : 
        IHandleEvent<BrandCreatedEvent>,
        IHandleEvent<BrandNameChangedEvent>,
        IHandleEvent<BrandUrlChangedEvent>,
        IHandleEvent<BrandDescriptionChangedEvent>,
        IHandleEvent<BrandDeletedEvent>,
        IHandleEvent<BrandRestoredEvent>
    {
        /// <summary>
        /// Get the event store
        /// </summary>
        public IEventStore EventStore { get; }

        /// <summary>
        /// Construct the event handler
        /// </summary>
        /// <param name="eventStore">The event store</param>
        public BrandEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
        }

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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
