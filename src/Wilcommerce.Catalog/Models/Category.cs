using System;
using System.Collections.Generic;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents a category
    /// </summary>
    public class Category : IAggregateRoot
    {
        /// <summary>
        /// The category id
        /// </summary>
        public Guid Id { get; set; }

        #region Constructor
        protected Category()
        {
            this._Children = new HashSet<Category>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// The category unique code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The category url (unique slug)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A description for the category
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether the category is visible
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// The date and time from which the category is visible
        /// </summary>
        public DateTime? VisibleFrom { get; set; }

        /// <summary>
        /// The date and time till which the category is visible
        /// </summary>
        public DateTime? VisibleTo { get; set; }

        /// <summary>
        /// Whether the category is deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The parent category
        /// </summary>
        public virtual Category Parent { get; set; }

        /// <summary>
        /// The list of children categories
        /// </summary>
        protected virtual ICollection<Category> _Children { get; set; }

        /// <summary>
        /// The list of childrend categories (Read only)
        /// </summary>
        public IEnumerable<Category> Children => _Children;

        #endregion

        #region Behaviors

        /// <summary>
        /// Set the category as visible, starting from the current date and time
        /// </summary>
        public void SetAsVisible()
        {
            this.SetAsVisible(DateTime.Now);
        }

        /// <summary>
        /// Set the category as visible
        /// </summary>
        /// <param name="from">The date and time from which the category is visible</param>
        public void SetAsVisible(DateTime from)
        {
            this.IsVisible = true;
            this.VisibleFrom = from;
        }

        /// <summary>
        /// Set the category as visible
        /// </summary>
        /// <param name="from">The date and time from which the category is visible</param>
        /// <param name="to">The date and time till which the category is visible</param>
        public void SetAsVisible(DateTime from, DateTime to)
        {
            if (from >= to)
            {
                throw new ArgumentException("the from date should be previous to the end date");
            }

            this.SetAsVisible(from);
            this.VisibleTo = to;
        }

        /// <summary>
        /// Add a children to the category
        /// </summary>
        /// <param name="children">The category children</param>
        public void AddChildren(Category children)
        {
            if(children == null)
            {
                throw new ArgumentNullException("children");
            }

            if (this._Children.Contains(children))
            {
                throw new ArgumentException("The category contains the children yet");
            }

            this._Children.Add(children);
        }

        #endregion

        #region Factory Methods
        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="code">The category unique code</param>
        /// <param name="name">The category name</param>
        /// <param name="url">The category url</param>
        /// <returns>The category created</returns>
        public static Category Create(string code, string name, string url)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Url = url
            };

            return category;
        }

        #endregion
    }
}
