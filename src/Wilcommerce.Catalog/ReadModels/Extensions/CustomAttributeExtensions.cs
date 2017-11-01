using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    /// <summary>
    /// Defines all the extension methods for the custom attribute read model
    /// </summary>
    public static class CustomAttributeExtensions
    {
        /// <summary>
        /// Retrieve all the custom attributes which are not deleted
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns>A list of custom attributes</returns>
        public static IQueryable<CustomAttribute> Active(this IQueryable<CustomAttribute> attributes)
        {
            return from a in attributes
                   where !a.Deleted
                   select a;
        }
    }
}
