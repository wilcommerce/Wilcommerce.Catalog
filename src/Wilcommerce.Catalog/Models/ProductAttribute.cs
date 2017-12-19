using Newtonsoft.Json;
using System;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represent a product custom attribute
    /// </summary>
    public class ProductAttribute
    {
        /// <summary>
        /// Get or set the attribute's id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Get the serialized value
        /// </summary>
        public string _Value { get; protected set; }

        /// <summary>
        /// Get or set the attribute's value
        /// </summary>
        public object Value
        {
            get => JsonConvert.DeserializeObject(_Value);
            set
            {
                _Value = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// Get or set the attribute's product reference
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Get or set the attribute's custom attribute reference
        /// </summary>
        public virtual CustomAttribute Attribute { get; set; }
    }
}
