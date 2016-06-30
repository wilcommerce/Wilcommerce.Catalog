using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Models
{
    public class CategoryTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CategoryFactory_Should_Throw_ArgumentNullException_If_Code_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Category.Create(
                value,
                "Test Category",
                "test-category"
                ));

            Assert.Equal("code", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CategoryFactory_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Category.Create(
                "TEST1",
                value,
                "test-category"
                ));

            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CategoryFactory_Should_Throw_ArgumentNullException_If_Url_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Category.Create(
                "TEST1",
                "Test Category",
                value
                ));

            Assert.Equal("url", ex.ParamName);
        }

        [Fact]
        public void Category_IsVisible_From_Today()
        {
            var today = DateTime.Now;

            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            category.SetAsVisible(today);

            Assert.Equal(true, category.IsVisible);
            Assert.Equal(today, category.VisibleFrom);
        }

        [Fact]
        public void Category_SetAsVisible_Should_Fail_If_VisibleTo_Is_Previous_Than_VisibleFrom()
        {
            var today = DateTime.Now;
            var yesterday = today.AddDays(-1);

            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<ArgumentException>(() => category.SetAsVisible(today, yesterday));

            Assert.Equal("the from date should be previous to the end date", ex.Message);
        }

        [Fact]
        public void Category_AddChildren_Should_Fail_If_Children_IsNull()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => category.AddChildren(null));

            Assert.Equal("children", ex.ParamName);
        }

        [Fact]
        public void Category_AddChildren_Should_Fail_If_Children_Is_Already_InCollection()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var children = Category.Create(
                "TEST2",
                "Children Category",
                "children-category"
                );

            category.AddChildren(children);

            var ex = Assert.Throws<ArgumentException>(() => category.AddChildren(children));
            Assert.Equal("The category contains the children yet", ex.Message);
        }

        [Fact]
        public void Category_AddChildren_Should_Increment_ChildrenNumber()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            int childrenCount = category.Children.Count();

            var children = Category.Create(
                "TEST2",
                "Children Category",
                "children-category"
                );

            category.AddChildren(children);

            Assert.Equal(childrenCount + 1, category.Children.Count());
        }
    }
}
