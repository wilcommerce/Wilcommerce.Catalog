﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product deleted
    /// </summary>
    public class ProductDeletedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="userId">The user's id</param>
        public ProductDeletedEvent(Guid productId, string userId)
            : base(productId, typeof(Models.Product), userId)
        {
            ProductId = productId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Product {ProductId} deleted";
        }
    }
}
