using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Catalog.ReadModels;
using Xunit;

namespace Wilcommerce.Catalog.Test.ReadModels
{
    public class CustomAttributeExtensionsTest
    {
        #region Active tests
        [Fact]
        public void Active_Should_Throw_ArgumentNullException_If_Attributes_Is_Empty()
        {
            IQueryable<CustomAttribute> attributes = null;

            var ex = Assert.Throws<ArgumentNullException>(() => CustomAttributeExtensions.Active(attributes));
            Assert.Equal(nameof(attributes), ex.ParamName);
        }

        [Fact]
        public void Active_Should_Return_Only_Attributes_Not_Deleted()
        {
            var a1 = CustomAttribute.Create("a1", "t1");
            var a2 = CustomAttribute.Create("a2", "t2");
            var a3 = CustomAttribute.Create("a3", "t3");
            a3.Delete();

            IQueryable<CustomAttribute> attributes = new CustomAttribute[]
            {
                a1, a2, a3
            }.AsQueryable();

            var activeAttributes = CustomAttributeExtensions.Active(attributes).ToArray();

            Assert.True(activeAttributes.All(a => !a.Deleted));
        }
        #endregion
    }
}
