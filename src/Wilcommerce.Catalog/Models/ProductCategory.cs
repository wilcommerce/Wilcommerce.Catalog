using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wilcommerce.Catalog.Models
{
    public class ProductCategory
    {
        public Guid ProductId { get; set; }

        public Guid CategoryId { get; set; }

        public bool IsMain { get; set; }

        public virtual Product Product { get; protected set; }

        public virtual Category Category { get; protected set; }
    }
}
