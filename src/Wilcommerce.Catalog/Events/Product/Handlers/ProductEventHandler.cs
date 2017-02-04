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
        IHandleEvent<ProductRemovedFromSaleEvent>,
        IHandleEvent<ProductVendorSetEvent>,
        IHandleEvent<ProductCategoryAddedEvent>,
        IHandleEvent<ProductVariantAddedEvent>,
        IHandleEvent<ProductVariantRemovedEvent>,
        IHandleEvent<ProductAttributeAddedEvent>,
        IHandleEvent<ProductAttributeRemovedEvent>,
        IHandleEvent<ProductTierPriceAddedEvent>,
        IHandleEvent<ProductTierPriceRemovedEvent>,
        IHandleEvent<ProductReviewAddedEvent>,
        IHandleEvent<ProductReviewApprovedEvent>,
        IHandleEvent<ProductReviewRemovedEvent>,
        IHandleEvent<ProductImageAddedEvent>,
        IHandleEvent<ProductImageRemovedEvent>,
        IHandleEvent<ProductTierPriceChangedEvent>
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

        public void Handle(ProductVendorSetEvent @event)
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

        public void Handle(ProductCategoryAddedEvent @event)
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

        public void Handle(ProductVariantAddedEvent @event)
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

        public void Handle(ProductVariantRemovedEvent @event)
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

        public void Handle(ProductAttributeAddedEvent @event)
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

        public void Handle(ProductAttributeRemovedEvent @event)
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

        public void Handle(ProductTierPriceAddedEvent @event)
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

        public void Handle(ProductTierPriceRemovedEvent @event)
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

        public void Handle(ProductReviewAddedEvent @event)
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

        public void Handle(ProductReviewApprovedEvent @event)
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

        public void Handle(ProductReviewRemovedEvent @event)
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

        public void Handle(ProductImageAddedEvent @event)
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

        public void Handle(ProductImageRemovedEvent @event)
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

        public void Handle(ProductTierPriceChangedEvent @event)
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
