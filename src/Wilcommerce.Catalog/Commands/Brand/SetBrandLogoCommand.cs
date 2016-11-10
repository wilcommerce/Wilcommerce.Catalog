using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands.Brand
{
    public class SetBrandLogoCommand : ICommand
    {
        public Guid BrandId { get; }

        public Image Logo { get; }

        public SetBrandLogoCommand(Guid brandId, Image logo)
        {
            BrandId = brandId;
            Logo = logo;
        }
    }
}
