using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands.Brand
{
    public class RestoreBrandCommand : ICommand
    {
        public Guid BrandId { get; }

        public RestoreBrandCommand(Guid brandId)
        {
            BrandId = brandId;
        }
    }
}
