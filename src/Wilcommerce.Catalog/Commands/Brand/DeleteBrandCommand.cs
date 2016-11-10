using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands.Brand
{
    public class DeleteBrandCommand : ICommand
    {
        public Guid BrandId { get; }

        public DeleteBrandCommand(Guid brandId)
        {
            BrandId = brandId;
        }
    }
}
