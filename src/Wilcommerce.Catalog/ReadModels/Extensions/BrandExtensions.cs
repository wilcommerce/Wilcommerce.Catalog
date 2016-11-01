using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    public static class BrandExtensions
    {
        public static IQueryable<Brand> Active(this IQueryable<Brand> brands)
        {
            return from b in brands
                   where !b.Deleted
                   select b;
        }
    }
}
