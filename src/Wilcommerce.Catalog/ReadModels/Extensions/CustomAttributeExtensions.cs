using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    public static class CustomAttributeExtensions
    {
        public static IQueryable<CustomAttribute> Active(this IQueryable<CustomAttribute> attributes)
        {
            return from a in attributes
                   where !a.Deleted
                   select a;
        }
    }
}
