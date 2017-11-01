using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    /// <summary>
    /// Defines all the extension methods for the category read model
    /// </summary>
    public static class CategoryExtensions
    {
        /// <summary>
        /// Retrieve all the categories which are not deleted
        /// </summary>
        /// <param name="categories"></param>
        /// <returns>A list of categories</returns>
        public static IQueryable<Category> Active(this IQueryable<Category> categories)
        {
            return from c in categories
                   where !c.Deleted
                   select c;
        }

        /// <summary>
        /// Retrieve all the categories filtered by the specified parent
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="parentId">The category parent id</param>
        /// <returns>A list of categories</returns>
        public static IQueryable<Category> ByParent(this IQueryable<Category> categories, Guid parentId)
        {
            return from c in categories
                   where c.Parent.Id == parentId
                   select c;
        }

        /// <summary>
        /// Retrieve all the categories which are visible starting from the specified date
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="fromDate">The filter date</param>
        /// <returns>A list of categories</returns>
        public static IQueryable<Category> VisibleFrom(this IQueryable<Category> categories, DateTime fromDate)
        {
            return from c in categories
                   where c.IsVisible && c.VisibleFrom >= fromDate
                   select c;
        }

        /// <summary>
        /// Retrieve all the categories which are visible till the specified date
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="tillDate">The filter date</param>
        /// <returns>A list of categories</returns>
        public static IQueryable<Category> VisibleTill(this IQueryable<Category> categories, DateTime tillDate)
        {
            return from c in categories
                   where c.IsVisible && c.VisibleTo <= tillDate
                   select c;
        }

        /// <summary>
        /// Retrieve all the categories which are visible at this moment
        /// </summary>
        /// <param name="categories"></param>
        /// <returns>A list of categories</returns>
        public static IQueryable<Category> Visible(this IQueryable<Category> categories)
        {
            var today = DateTime.Now;

            return from c in categories
                   where c.IsVisible && (c.VisibleFrom <= today && c.VisibleTo >= today)
                   select c;
        }
    }
}
