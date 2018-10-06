using System;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Events.Category;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Core.Common.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands
{
    /// <summary>
    /// Implementation of <see cref="ICategoryCommands"/>
    /// </summary>
    public class CategoryCommands : ICategoryCommands
    {
        /// <summary>
        /// Get the repository
        /// </summary>
        public Repository.IRepository Repository { get; }

        /// <summary>
        /// Get the event bus
        /// </summary>
        public IEventBus EventBus { get; }

        /// <summary>
        /// Construct the category commands
        /// </summary>
        /// <param name="repository">The repository</param>
        /// <param name="eventBus">The event bus</param>
        public CategoryCommands(Repository.IRepository repository, IEventBus eventBus)
        {
            Repository = repository;
            EventBus = eventBus;
        }

        #region Category Commands
        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.CreateNewCategory(string, string, string, string, bool, DateTime?, DateTime?)"/>
        /// </summary>
        /// <param name="code">The category code</param>
        /// <param name="name">The category name</param>
        /// <param name="url">The category url</param>
        /// <param name="description">The category description</param>
        /// <param name="isVisible">Whether the category is visible</param>
        /// <param name="visibleFrom">The date and time of when the category starts to be visible</param>
        /// <param name="visibleTo">The date and time till when the category is visible</param>
        /// <returns>The category id</returns>
        public async Task<Guid> CreateNewCategory(string code, string name, string url, string description, bool isVisible, DateTime? visibleFrom, DateTime? visibleTo)
        {
            try
            {
                var category = Category.Create(code, name, url);
                if (!string.IsNullOrEmpty(description))
                {
                    category.ChangeDescription(description);
                }

                if (isVisible)
                {
                    if (visibleFrom == null)
                    {
                        category.SetAsVisible();
                    }
                    else if (visibleTo == null)
                    {
                        category.SetAsVisible((DateTime)visibleFrom);
                    }
                    else
                    {
                        category.SetAsVisible((DateTime)visibleFrom, (DateTime)visibleTo);
                    }
                }

                Repository.Add(category);
                await Repository.SaveChangesAsync();

                var @event = new CategoryCreatedEvent(category.Id, name, code);
                EventBus.RaiseEvent(@event);

                return category.Id;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.UpdateCategoryInfo(Guid, string, string, string, string)"/>
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="code">The category code</param>
        /// <param name="name">The category name</param>
        /// <param name="url">The category url</param>
        /// <param name="description">The category description</param>
        /// <returns></returns>
        public async Task UpdateCategoryInfo(Guid categoryId, string code, string name, string url, string description)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                if (category.Code != code)
                {
                    category.ChangeCode(code);
                }

                if (category.Name != name)
                {
                    category.ChangeName(name);
                }

                if (category.Url != url)
                {
                    category.ChangeUrl(url);
                }

                if (category.Description != description)
                {
                    category.ChangeDescription(description);
                }

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.SetCategoryAsVisible(Guid, DateTime?, DateTime?)"/>
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="from">The date and time of when the category starts to be visible</param>
        /// <param name="to">The date and time till when the category is visible</param>
        /// <returns></returns>
        public async Task SetCategoryAsVisible(Guid categoryId, DateTime? from, DateTime? to)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                if (from == null)
                {
                    category.SetAsVisible();
                }
                else if (to == null)
                {
                    category.SetAsVisible((DateTime)from);
                }
                else
                {
                    category.SetAsVisible((DateTime)from, (DateTime)to);
                }

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.HideCategory(Guid)"/>
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        public async Task HideCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.Hide();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.AddCategoryChild(Guid, Guid)"/>
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="childId">The child id</param>
        /// <returns></returns>
        public async Task AddCategoryChild(Guid categoryId, Guid childId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                var child = await Repository.GetByKeyAsync<Category>(childId);

                category.AddChild(child);
                await Repository.SaveChangesAsync();

                var @event = new CategoryChildAddedEvent(categoryId, childId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.SetParentForCategory(Guid, Guid)"/>
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="parentId">The parent category id</param>
        /// <returns></returns>
        public async Task SetParentForCategory(Guid categoryId, Guid parentId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                var parent = await Repository.GetByKeyAsync<Category>(parentId);
                category.SetParentCategory(parent);

                await Repository.SaveChangesAsync();

                var @event = new CategoryChildAddedEvent(parentId, categoryId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.DeleteCategory(Guid)"/>
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        public async Task DeleteCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.Delete();

                await Repository.SaveChangesAsync();

                var @event = new CategoryDeletedEvent(categoryId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.RestoreCategory(Guid)"/>
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        public async Task RestoreCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.Restore();

                await Repository.SaveChangesAsync();

                var @event = new CategoryRestoredEvent(categoryId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.RemoveChildForCategory(Guid, Guid)"/>
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="childId">The child to remove</param>
        /// <returns></returns>
        public async Task RemoveChildForCategory(Guid categoryId, Guid childId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.RemoveChild(childId);

                await Repository.SaveChangesAsync();

                var @event = new CategoryChildRemovedEvent(categoryId, childId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.RemoveParentForCategory(Guid)"/>
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        public async Task RemoveParentForCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                var parentId = category.Parent?.Id;

                category.RemoveParent();

                await Repository.SaveChangesAsync();

                if (parentId != null)
                {
                    var @event = new CategoryChildRemovedEvent((Guid)parentId, categoryId);
                    EventBus.RaiseEvent(@event);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICategoryCommands.SetCategorySeoData(Guid, SeoData)"/>
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="seo">The seo information</param>
        /// <returns></returns>
        public async Task SetCategorySeoData(Guid categoryId, SeoData seo)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.SetSeoData(seo);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
