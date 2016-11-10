using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands.Brand
{
    public class ChangeBrandDescriptionCommand : ICommand
    {
        public Guid BrandId { get; }

        public string Description { get; }

        public ChangeBrandDescriptionCommand(Guid brandId, string description)
        {
            BrandId = brandId;
            Description = description;
        }
    }
}
