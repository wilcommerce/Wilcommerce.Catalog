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
        [InlineData(" ")]
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
        [InlineData(" ")]
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
        [InlineData(" ")]
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

            category.SetAsVisible(today, null);

            Assert.True(category.IsVisible);
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
        public void Category_AddChild_Should_Fail_If_Children_IsNull()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => category.AddChild(null));

            Assert.Equal("child", ex.ParamName);
        }

        [Fact]
        public void Category_AddChild_Should_Fail_If_Children_Is_Already_InCollection()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var child = Category.Create(
                "TEST2",
                "Children Category",
                "children-category"
                );

            category.AddChild(child);

            var ex = Assert.Throws<ArgumentException>(() => category.AddChild(child));
            Assert.Equal("The category contains the children yet", ex.Message);
        }

        [Fact]
        public void Category_AddChild_Should_Increment_ChildrenNumber()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            int childrenCount = category.Children.Count();

            var child = Category.Create(
                "TEST2",
                "Children Category",
                "children-category"
                );

            category.AddChild(child);

            Assert.Equal(childrenCount + 1, category.Children.Count());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
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
        [InlineData(" ")]
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
        [InlineData(" ")]
        public void ChangeDescription_Should_Clear_If_Description_IsEmpty(string value)
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            category.ChangeDescription(value);

            Assert.True(string.IsNullOrWhiteSpace(category.Description));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
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

        [Fact]
        public void SetSeoData_Should_Throw_ArgumentNullException_If_Seo_IsNull()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => category.SetSeoData(null));
            Assert.Equal("seo", ex.ParamName);
        }

        [Fact]
        public void Hide_Should_Throw_InvalidOperationException_If_Category_IsNotVisible()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => category.Hide());
            Assert.Equal("The category is not visible", ex.Message);
        }

        [Fact]
        public void Hide_Should_Set_IsVisible_To_False()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            category.SetAsVisible();
            category.Hide();

            Assert.False(category.IsVisible);
            Assert.Equal(DateTime.Now.ToString("yyyy-MM-dd"), category.VisibleTo?.ToString("yyyy-MM-dd"));
        }

        [Fact]
        public void RemoveChild_Should_Remove_The_Specified_Category()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var child = Category.Create(
                "TEST1.1",
                "Child category",
                "child-category");

            category.AddChild(child);
            category.RemoveChild(child);

            Assert.Empty(category.Children);
        }

        [Fact]
        public void RemoveChild_Should_Throw_InvalidOperationException_If_Child_Cannot_Be_Removed()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var child = Category.Create(
                "TEST1.1",
                "Child category",
                "child-category");

            var ex = Assert.Throws<InvalidOperationException>(() => category.RemoveChild(child));

            Assert.Equal($"Cannot remove child {child.Id}", ex.Message);
        }

        [Fact]
        public void RemoveParent_Should_Throw_ArgumentNullException_If_Parent_Is_Null()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            Category parent = null;

            var ex = Assert.Throws<ArgumentNullException>(() => category.RemoveParent(parent));
            Assert.Equal(nameof(parent), ex.ParamName);
        }

        [Fact]
        public void RemoveParent_Should_Throw_ArgumentException_If_Specified_Parent_Is_Different_From_Current_Category_Parent()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var parent = Category.Create("PARENT1", "Parent", "parent");
            category.SetParentCategory(parent);

            parent = Category.Create("WRONG", "Wrong parent", "wrong-parent");

            var ex = Assert.Throws<ArgumentException>(() => category.RemoveParent(parent));
            Assert.Equal(nameof(parent), ex.ParamName);
        }

        [Fact]
        public void RemoveParent_Should_Throw_InvalidOperationException_If_Current_Parent_Is_Already_Empty()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var parent = Category.Create("PARENT1", "Parent", "parent");

            var ex = Assert.Throws<InvalidOperationException>(() => category.RemoveParent(parent));
            Assert.Equal("Parent already empty", ex.Message);
        }

        [Fact]
        public void RemoveParent_Should_Set_Parent_To_Null()
        {
            var category = Category.Create(
                "TEST1",
                "Test Category",
                "test-category"
                );

            var parent = Category.Create("PARENT1", "Parent", "parent");

            category.SetParentCategory(parent);
            category.RemoveParent(parent);

            Assert.Null(category.Parent);
        }
    }
}
