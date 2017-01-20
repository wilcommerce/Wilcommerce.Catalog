using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product.Handlers
{
    public class ProductEventHandler :
        IHandleEvent<ProductCreatedEvent>,
        IHandleEvent<ProductDeletedEvent>,
        IHandleEvent<ProductRestoredEvent>,
        IHandleEvent<ProductUnitInStockChangedEvent>,
        IHandleEvent<ProductEanCodeChangedEvent>,
        IHandleEvent<ProductSkuChangedEvent>,
        IHandleEvent<ProductNameChangedEvent>,
        IHandleEvent<ProductDescriptionChangedEvent>,
        IHandleEvent<ProductUrlChangedEvent>,
        IHandleEvent<ProductPriceSetEvent>,
        IHandleEvent<ProductSetOnSaleEvent>,
        IHandleEvent<ProductRemovedFromSaleEvent>
    {
        public IEventStore EventStore { get; }

        public ProductEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
        }

        public void Handle(ProductCreatedEvent @event)
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

        public void Handle(ProductDeletedEvent @event)
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

        public void Handle(ProductRestoredEvent @event)
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

        public void Handle(ProductUnitInStockChangedEvent @event)
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

        public void Handle(ProductEanCodeChangedEvent @event)
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

        public void Handle(ProductSkuChangedEvent @event)
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

        public void Handle(ProductNameChangedEvent @event)
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

        public void Handle(ProductDescriptionChangedEvent @event)
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

        public void Handle(ProductUrlChangedEvent @event)
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

        public void Handle(ProductPriceSetEvent @event)
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

        public void Handle(ProductSetOnSaleEvent @event)
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

        public void Handle(ProductRemovedFromSaleEvent @event)
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
