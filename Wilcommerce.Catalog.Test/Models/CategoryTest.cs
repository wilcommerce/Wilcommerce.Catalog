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

            var ex = Assert.Throws<ArgumentNullException>(() => category.AddChild(null));

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

            category.AddChild(children);

            var ex = Assert.Throws<ArgumentException>(() => category.AddChild(children));
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

            category.AddChild(children);

            Assert.Equal(childrenCount + 1, category.Children.Count());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeName_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => category.ChangeName(value));
            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeCode_Should_Throw_ArgumentNullException_If_Code_IsEmpty(string value)
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => category.ChangeCode(value));
            Assert.Equal("code", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeDescription_Should_Throw_ArgumentNullException_If_Description_IsEmpty(string value)
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => category.ChangeDescription(value));
            Assert.Equal("description", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeUrl_Should_Throw_ArgumentNullException_If_Url_IsEmpty(string value)
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => category.ChangeUrl(value));
            Assert.Equal("url", ex.ParamName);
        }

        [Fact]
        public void SetParentCategory_Should_Throw_ArgumentNullException_If_Parent_IsNull()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => category.SetParentCategory(null));
            Assert.Equal("parent", ex.ParamName);
        }

        [Fact]
        public void Delete_Should_Throw_InvalidOperationException_If_Category_IsDeleted()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            category.Delete();

            var ex = Assert.Throws<InvalidOperationException>(() => category.Delete());
            Assert.Equal("The category is already deleted", ex.Message);
        }

        [Fact]
        public void Restore_Should_Throw_InvalidOperationException_If_Category_IsNotDeleted()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => category.Restore());
            Assert.Equal("The category is not deleted", ex.Message);
        }
    }
}
