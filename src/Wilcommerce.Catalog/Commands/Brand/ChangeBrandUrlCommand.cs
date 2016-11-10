using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands.Brand
{
    public class ChangeBrandUrlCommand : ICommand
    {
        public Guid BrandId { get; }

        public string Url { get; }

        public ChangeBrandUrlCommand(Guid brandId, string url)
        {
            BrandId = brandId;
            Url = url;
        }
    }
}
