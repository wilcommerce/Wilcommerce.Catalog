using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands.Brand
{
    public class ChangeBrandNameCommand : ICommand
    {
        public Guid BrandId { get; }

        public string Name { get; }

        public ChangeBrandNameCommand(Guid brandId, string name)
        {
            BrandId = brandId;
            Name = name;
        }
    }
}
