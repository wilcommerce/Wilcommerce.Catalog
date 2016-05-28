using System;

namespace Wilcommerce.Catalog.Models
{
    public class ProductAttribute
    {
        public Guid Id { get; set; }

        public object Value { get; set; }

        public virtual Product Product { get; set; }

        public virtual CustomAttribute Attribute { get; set; }
    }
}
