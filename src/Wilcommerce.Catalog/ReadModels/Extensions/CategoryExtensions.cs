using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    public static class CategoryExtensions
    {
        public static IQueryable<Category> Active(this IQueryable<Category> categories)
        {
            return from c in categories
                   where !c.Deleted
                   select c;
        }

        public static IQueryable<Category> ByParent(this IQueryable<Category> categories, Guid parentId)
        {
            return from c in categories
                   where c.Parent.Id == parentId
                   select c;
        }

        public static IQueryable<Category> VisibleFrom(this IQueryable<Category> categories, DateTime fromDate)
        {
            return from c in categories
                   where c.IsVisible && c.VisibleFrom >= fromDate
                   select c;
        }

        public static IQueryable<Category> VisibleTill(this IQueryable<Category> categories, DateTime tillDate)
        {
            return from c in categories
                   where c.IsVisible && c.VisibleTo <= tillDate
                   select c;
        }

        public static IQueryable<Category> Visible(this IQueryable<Category> categories)
        {
            var today = DateTime.Now;

            return from c in categories
                   where c.IsVisible && (c.VisibleFrom <= today && c.VisibleTo >= today)
                   select c;
        }
    }
}
