using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents the general settings for the catalog module
    /// </summary>
    public class CatalogSettings : IAggregateRoot
    {
        /// <summary>
        /// Get or set the settings id
        /// </summary>
        public Guid Id { get; set; }

        #region Constructor
        protected CatalogSettings() { }
        #endregion

        #region Properties
        /// <summary>
        /// Get whether the prices are shown
        /// </summary>
        public bool PricesShown { get; protected set; }

        /// <summary>
        /// Get whether the product reviews are allowed
        /// </summary>
        public bool ProductReviewsAllowed { get; protected set; }

        /// <summary>
        /// Get the number of categories per page
        /// </summary>
        public int CategoriesPerPage { get; protected set; }

        /// <summary>
        /// Get the number of products per page
        /// </summary>
        public int ProductsPerPage { get; protected set; }

        /// <summary>
        /// Get the number of reviews per page
        /// </summary>
        public int ProductReviewsPerPage { get; protected set; }

        /// <summary>
        /// Get the view type for the categories
        /// </summary>
        public ViewType CategoriesViewType { get; protected set; }

        /// <summary>
        /// Get the view type for the categories
        /// </summary>
        public ViewType ProductsViewType { get; protected set; }

        #endregion

        #region Behaviors
        /// <summary>
        /// Set whether show or not the prices
        /// </summary>
        /// <param name="showPrices">Whether show or not the prices</param>
        public virtual void ShowPrices(bool showPrices)
        {
            PricesShown = showPrices;
        }

        /// <summary>
        /// Set whether allow or not the product reviews
        /// </summary>
        /// <param name="allowed">Whether allow or not the product reviews</param>
        public virtual void AllowProductReviews(bool allowed)
        {
            ProductReviewsAllowed = allowed;
        }

        /// <summary>
        /// Set the number of categories per page
        /// </summary>
        /// <param name="categoriesPerPage">The number of categories per page</param>
        public virtual void SetCategoriesPerPage(int categoriesPerPage)
        {
            if (categoriesPerPage < 0)
            {
                throw new ArgumentException("category per page cannot be less than zero");
            }

            CategoriesPerPage = categoriesPerPage;
        }

        /// <summary>
        /// Set the number of products per page
        /// </summary>
        /// <param name="productsPerPage">The number of products per page</param>
        public virtual void SetProductsPerPage(int productsPerPage)
        {
            if (productsPerPage < 0)
            {
                throw new ArgumentException("products per page cannot be less than zero");
            }

            ProductsPerPage = productsPerPage;
        }

        /// <summary>
        /// Set the number of reviews per page
        /// </summary>
        /// <param name="reviewsPerPage">The number of product reviews per page</param>
        public virtual void SetProductReviewsPerPage(int reviewsPerPage)
        {
            if (reviewsPerPage <  0)
            {
                throw new ArgumentException("reviews per page cannot be less than zero");
            }

            if (!ProductReviewsAllowed)
            {
                throw new InvalidOperationException("Reviews not allowed");
            }

            ProductReviewsPerPage = reviewsPerPage;
        }

        /// <summary>
        /// Set the categories view type
        /// </summary>
        /// <param name="viewType">The categories view type</param>
        public virtual void SetCategoriesViewType(ViewType viewType)
        {
            CategoriesViewType = viewType;
        }

        /// <summary>
        /// Set the products view type
        /// </summary>
        /// <param name="viewType">The products view type</param>
        public virtual void SetProductsViewType(ViewType viewType)
        {
            ProductsViewType = viewType;
        }

        #endregion

        #region Factory Method
        /// <summary>
        /// Create a catalog settings
        /// </summary>
        /// <param name="categoriesPerPage">The number of categories per page</param>
        /// <param name="productsPerPage">The number of products per page</param>
        /// <param name="categoriesViewType">The categories view type</param>
        /// <param name="productsViewType">The products view type</param>
        /// <returns>The new catalog settings</returns>
        public static CatalogSettings Create(int categoriesPerPage, int productsPerPage, ViewType categoriesViewType, ViewType productsViewType)
        {
            if (categoriesPerPage < 0)
            {
                throw new ArgumentException("category per page cannot be less than zero");
            }

            if (productsPerPage < 0)
            {
                throw new ArgumentException("products per page cannot be less than zero");
            }

            return new CatalogSettings
            {
                Id = Guid.NewGuid(),
                CategoriesPerPage = categoriesPerPage,
                ProductsPerPage = productsPerPage,
                CategoriesViewType = categoriesViewType,
                ProductsViewType = productsViewType
            };
        }

        #endregion

        #region Enums
        /// <summary>
        /// Represents a view type (LIST, MANSORY, etc.)
        /// </summary>
        public enum ViewType
        {
            LIST,
            MANSORY
        }
        #endregion
    }
}
