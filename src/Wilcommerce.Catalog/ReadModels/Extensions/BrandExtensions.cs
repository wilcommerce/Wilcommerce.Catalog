using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    /// <summary>
    /// Defines the extension methods for the brand read model
    /// </summary>
    public static class BrandExtensions
    {
        /// <summary>
        /// Retrieve all the brands which are not deleted
        /// </summary>
        /// <param name="brands"></param>
        /// <returns>A list of brands</returns>
        public static IQueryable<Brand> Active(this IQueryable<Brand> brands)
        {
            return from b in brands
                   where !b.Deleted
                   select b;
        }
    }
}
