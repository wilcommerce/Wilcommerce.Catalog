using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Catalog.ReadModels;
using Xunit;

namespace Wilcommerce.Catalog.Test.ReadModels
{
    public class CategoryExtensionsTest
    {
        #region Active tests
        [Fact]
        public void Active_Should_Throw_ArgumentNullException_If_Categories_Is_Null()
        {
            IQueryable<Category> categories = null;

            var ex = Assert.Throws<ArgumentNullException>(() => CategoryExtensions.Active(categories));
            Assert.Equal(nameof(categories), ex.ParamName);
        }

        [Fact]
        public void Active_Should_Return_Only_Categories_Not_Deleted()
        {
            var c1 = Category.Create("c1", "c1", "c1");
            var c2 = Category.Create("c2", "c2", "c2");
            var c3 = Category.Create("c3", "c3", "c3");
            c3.Delete();

            IQueryable<Category> categories = new Category[]
            {
                c1, c2, c3
            }.AsQueryable();

            var activeCategories = CategoryExtensions.Active(categories).ToArray();
            Assert.True(activeCategories.All(c => !c.Deleted));
        }
        #endregion

        #region ByParent tests
        [Fact]
        public void ByParent_Should_Throw_ArgumentNullException_If_Categories_Is_Null()
        {
            IQueryable<Category> categories = null;
            Guid parentId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() => CategoryExtensions.ByParent(categories, parentId));
            Assert.Equal(nameof(categories), ex.ParamName);
        }

        [Fact]
        public void ByParent_Should_Throw_ArgumentException_If_ParentId_Is_Empty()
        {
            var c1 = Category.Create("c1", "c1", "c1");
            var c2 = Category.Create("c2", "c2", "c2");
            var c3 = Category.Create("c3", "c3", "c3");

            IQueryable<Category> categories = new Category[]
            {
                c1, c2, c3
            }.AsQueryable();
            Guid parentId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => CategoryExtensions.ByParent(categories, parentId));
            Assert.Equal(nameof(parentId), ex.ParamName);
        }

        [Fact]
        public void ByParent_Should_Return_Only_Categories_With_The_Specified_Parent()
        {
            var c1 = Category.Create("c1", "c1", "c1");
            var c2 = Category.Create("c2", "c2", "c2");
            var c3 = Category.Create("c3", "c3", "c3");

            c2.SetParentCategory(c3);

            IQueryable<Category> categories = new Category[]
            {
                c1, c2
            }.AsQueryable();
            Guid parentId = c3.Id;

            var categoriesWithParent = CategoryExtensions.ByParent(categories, parentId).ToArray();

            Assert.True(categoriesWithParent.All(c => c.Parent.Id == parentId));
        }
        #endregion

        #region VisibleFrom tests
        [Fact]
        public void VisibleFrom_Should_Throw_ArgumentNullException_If_Categories_Is_Null()
        {
            IQueryable<Category> categories = null;
            DateTime fromDate = DateTime.Today;

            var ex = Assert.Throws<ArgumentNullException>(() => CategoryExtensions.VisibleFrom(categories, fromDate));
            Assert.Equal(nameof(categories), ex.ParamName);
        }

        [Fact]
        public void VisibleFrom_Should_Returns_Only_Categories_With_VisibleFrom_Date_Greater_Than_The_Specified_Date()
        {
            DateTime fromDate = DateTime.Today;

            var c1 = Category.Create("c1", "c1", "c1");
            var c2 = Category.Create("c2", "c2", "c2");
            var c3 = Category.Create("c3", "c3", "c3");

            c1.SetAsVisible(fromDate, null);
            c2.SetAsVisible();

            IQueryable<Category> categories = new Category[]
            {
                c1, c2, c3
            }.AsQueryable();

            var categoriesVisible = CategoryExtensions.VisibleFrom(categories, fromDate).ToArray();
            Assert.True(categoriesVisible.All(c => c.VisibleFrom >= fromDate && c.IsVisible));
        }
        #endregion

        #region VisibleTill tests
        [Fact]
        public void VisibleTill_Should_Throw_ArgumentNullException_If_Categories_Is_Null()
        {
            IQueryable<Category> categories = null;
            DateTime tillDate = DateTime.Today;

            var ex = Assert.Throws<ArgumentNullException>(() => CategoryExtensions.VisibleTill(categories, tillDate));
            Assert.Equal(nameof(categories), ex.ParamName);
        }

        [Fact]
        public void VisibleTill_Should_Returns_Only_Categories_With_VisibleTo_Date_Previous_Than_The_Specified_Date()
        {
            DateTime tillDate = DateTime.Today.AddDays(3);

            var c1 = Category.Create("c1", "c1", "c1");
            var c2 = Category.Create("c2", "c2", "c2");
            var c3 = Category.Create("c3", "c3", "c3");

            c1.SetAsVisible(DateTime.Today, tillDate);
            c2.SetAsVisible(DateTime.Today, DateTime.Today.AddDays(1));

            IQueryable<Category> categories = new Category[]
            {
                c1, c2, c3
            }.AsQueryable();

            var categoriesVisible = CategoryExtensions.VisibleFrom(categories, tillDate).ToArray();
            Assert.True(categoriesVisible.All(c => c.VisibleTo <= tillDate && c.IsVisible));
        }
        #endregion

        #region Visible tests
        [Fact]
        public void Visible_Should_Throw_ArgumentNullException_If_Categories_Is_Null()
        {
            IQueryable<Category> categories = null;

            var ex = Assert.Throws<ArgumentNullException>(() => CategoryExtensions.Visible(categories));
            Assert.Equal(nameof(categories), ex.ParamName);
        }

        [Fact]
        public void Visible_Should_Return_Only_Categories_Where_Today_Date_Is_Between_VisibleFrom_And_VisibleTo()
        {
            var c1 = Category.Create("c1", "c1", "c1");
            var c2 = Category.Create("c2", "c2", "c2");
            var c3 = Category.Create("c3", "c3", "c3");

            var today = DateTime.Today;

            c1.SetAsVisible();
            c2.SetAsVisible(today, today.AddDays(2));

            IQueryable<Category> categories = new Category[]
            {
                c1, c2, c3
            }.AsQueryable();

            var visibleCategories = CategoryExtensions.Visible(categories);

            var now = DateTime.Now;
            Assert.True(visibleCategories.All(c => c.IsVisible && c.VisibleFrom <= now && c.VisibleTo >= now));
        }
        #endregion
    }
}
