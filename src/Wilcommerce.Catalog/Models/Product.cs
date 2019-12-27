using System;
using System.Collections.Generic;
using System.Linq;
using Wilcommerce.Core.Common.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product : IAggregateRoot
    {
        /// <summary>
        /// Get or set the product's id
        /// </summary>
        public Guid Id { get; protected set; }

        #region Constructor
        /// <summary>
        /// Construct the product
        /// </summary>
        protected Product()
        {
            this.Variants = new HashSet<Product>();
            this.ProductCategories = new HashSet<ProductCategory>();
            this.TierPrices = new HashSet<TierPrice>();
            this.Attributes = new HashSet<ProductAttribute>();
            this.Reviews = new HashSet<ProductReview>();
            this.Images = new HashSet<ProductImage>();
            Price = new Currency();
            Seo = new SeoData();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the EAN code for the product
        /// </summary>
        public string EanCode { get; protected set; }

        /// <summary>
        /// Get or set the SKU code for the product
        /// </summary>
        public string Sku { get; protected set; }

        /// <summary>
        /// Get or set the product's name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Get or set the product's url
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Get or set the product's description
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Get or set the product's price
        /// </summary>
        public Currency Price { get; protected set; }

        /// <summary>
        /// Get or set the number of units in stock
        /// </summary>
        public int UnitInStock { get; protected set; }

        /// <summary>
        /// Get or set whether the product is on sale
        /// </summary>
        public bool IsOnSale { get; protected set; }

        /// <summary>
        /// Get or set the date and time from when the product is on sale
        /// </summary>
        public DateTime? OnSaleFrom { get; protected set; }

        /// <summary>
        /// Get or set the date and time till when the product is on sale
        /// </summary>
        public DateTime? OnSaleTo { get; protected set; }

        /// <summary>
        /// Get the product's variants
        /// </summary>
        public virtual ICollection<Product> Variants { get; protected set; }

        /// <summary>
        /// Get or set the main product
        /// </summary>
        public virtual Product MainProduct { get; protected set; }

        /// <summary>
        /// Get or set the product's vendor
        /// </summary>
        public virtual Brand Vendor { get; protected set; }

        /// <summary>
        /// Get the associated category
        /// </summary>
        public virtual ICollection<ProductCategory> ProductCategories { get; protected set; }

        /// <summary>
        /// Get the product's category
        /// </summary>
        public IEnumerable<Category> Categories => ProductCategories.Select(c => c.Category);

        /// <summary>
        /// Get the main category for the product
        /// </summary>
        public Category MainCategory => ProductCategories.FirstOrDefault(c => c.IsMain)?.Category;

        /// <summary>
        /// Get the product's custom attributes
        /// </summary>
        public virtual ICollection<ProductAttribute> Attributes { get; protected set; }

        /// <summary>
        /// Get or set whether the product is deleted
        /// </summary>
        public bool Deleted { get; protected set; }

        /// <summary>
        /// Get or set whether the tier prices are enabled for the product
        /// </summary>
        public bool TierPriceEnabled { get; protected set; }

        /// <summary>
        /// Get the product's tier prices
        /// </summary>
        public virtual ICollection<TierPrice> TierPrices { get; protected set; }

        /// <summary>
        /// Get the product's reviews
        /// </summary>
        public virtual ICollection<ProductReview> Reviews { get; protected set; }

        /// <summary>
        /// Get the product's images
        /// </summary>
        public virtual ICollection<ProductImage> Images { get; protected set; }

        /// <summary>
        /// Get or set the SEO information for the product
        /// </summary>
        public virtual SeoData Seo { get; protected set; }

        /// <summary>
        /// Get the creation date
        /// </summary>
        public DateTime CreationDate { get; protected set; }
        #endregion

        #region Behaviors
        /// <summary>
        /// Delete the product
        /// </summary>
        public virtual void Delete()
        {
            if (Deleted)
            {
                throw new InvalidOperationException("Product already deleted");
            }

            Deleted = true;
        }

        /// <summary>
        /// Restore the deleted protected
        /// </summary>
        public virtual void Restore()
        {
            if (!Deleted)
            {
                throw new InvalidOperationException("Product is not deleted");
            }

            Deleted = false;
        }

        /// <summary>
        /// Set the unit in stock for the product
        /// </summary>
        /// <param name="unitInStock">The product's unit in stock</param>
        public virtual void SetUnitInStock(int unitInStock)
        {
            if (unitInStock < 0)
            {
                throw new ArgumentException("Stock unit cannot be less than zero", nameof(unitInStock));
            }

            UnitInStock = unitInStock;
        }

        /// <summary>
        /// Add product unit to the stock
        /// </summary>
        /// <param name="unit">The number of unit to add</param>
        public virtual void AddUnitInStock(int unit)
        {
            if (unit < 0)
            {
                throw new ArgumentException("Stock unit cannot be less than zero", nameof(unit));
            }

            SetUnitInStock(UnitInStock + unit);
        }

        /// <summary>
        /// Remove product unit from stock
        /// </summary>
        /// <param name="unit">The number of unit to remove</param>
        public virtual void RemoveUnitFromStock(int unit)
        {
            if (unit < 0)
            {
                throw new ArgumentException("Stock unit cannot be less than zero", nameof(unit));
            }

            int newUnitInStock = UnitInStock - unit;
            if (newUnitInStock < 0)
            {
                throw new InvalidOperationException("Stock unit cannot be less than zero");
            }

            SetUnitInStock(newUnitInStock);
        }

        /// <summary>
        /// Change the product EAN code
        /// </summary>
        /// <param name="ean">The product's ean code</param>
        public virtual void ChangeEanCode(string ean)
        {
            if (string.IsNullOrWhiteSpace(ean))
            {
                throw new ArgumentNullException(nameof(ean));
            }

            EanCode = ean;
        }

        /// <summary>
        /// Change the product SKU
        /// </summary>
        /// <param name="sku">The product's sku</param>
        public virtual void ChangeSku(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentNullException(nameof(sku));
            }

            Sku = sku;
        }

        /// <summary>
        /// Change the product name
        /// </summary>
        /// <param name="name">The product's name</param>
        public virtual void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Change the product description
        /// </summary>
        /// <param name="description">The product's description</param>
        public virtual void ChangeDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Change the product url
        /// </summary>
        /// <param name="url">The product's url</param>
        public virtual void ChangeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            Url = url;
        }

        /// <summary>
        /// Set the product price
        /// </summary>
        /// <param name="price">The new price</param>
        public virtual void SetPrice(Currency price)
        {
            if (price == null)
            {
                throw new ArgumentNullException(nameof(price));
            }

            if (price.Amount < 0)
            {
                throw new ArgumentException("Price amount cannot be less than zero", nameof(price));
            }

            Price = price;
        }

        /// <summary>
        /// Enables the tier prices
        /// </summary>
        public virtual void EnableTierPrices()
        {
            if (TierPriceEnabled)
            {
                throw new InvalidOperationException("Tier prices already enabled");
            }

            TierPriceEnabled = true;
        }

        /// <summary>
        /// Disable the tier prices
        /// </summary>
        public virtual void DisableTierPrices()
        {
            if (!TierPriceEnabled)
            {
                throw new InvalidOperationException("Tier prices already disabled");
            }

            TierPriceEnabled = false;
        }

        /// <summary>
        /// Set the product vendor
        /// </summary>
        /// <param name="vendor">The product's vendor</param>
        public virtual void SetVendor(Brand vendor)
        {
            Vendor = vendor ?? throw new ArgumentNullException(nameof(vendor));
        }

        /// <summary>
        /// Set the product on sale
        /// </summary>
        public virtual void SetOnSale()
        {
            SetOnSale(DateTime.Now, null);
        }

        /// <summary>
        /// Set the product on sale, starting from the specified start date and till the specified end date
        /// </summary>
        /// <param name="onSaleFrom">The date and time from when the product will be on sale</param>
        /// <param name="onSaleTo">The date and time till when the product will be on sale</param>
        public virtual void SetOnSale(DateTime? onSaleFrom, DateTime? onSaleTo)
        {
            if (onSaleFrom >= onSaleTo)
            {
                throw new ArgumentException("The sale's start date must be precedent to the end date");
            }

            IsOnSale = true;
            OnSaleFrom = onSaleFrom ?? DateTime.Now;
            OnSaleTo = onSaleTo;
        }

        /// <summary>
        /// Remove the product from the sales
        /// </summary>
        public virtual void RemoveFromSale()
        {
            RemoveFromSale(DateTime.Now);
        }

        /// <summary>
        /// Remove the product from the sales, starting from the specified date
        /// </summary>
        /// <param name="endSaleDate">The date and time from when the product will not be on sale anymore</param>
        public virtual void RemoveFromSale(DateTime endSaleDate)
        {
            IsOnSale = false;
            OnSaleTo = endSaleDate;
        }

        /// <summary>
        /// Add a category to the product
        /// </summary>
        /// <param name="category">The category to add</param>
        public virtual void AddCategory(Category category)
        {
            AddCategory(category, false);
        }

        /// <summary>
        /// Add a category to the product
        /// </summary>
        /// <param name="category">The category to add</param>
        /// <param name="isMain">Whether the category is the main category</param>
        public virtual void AddCategory(Category category, bool isMain)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (this.ProductCategories.Any(c => c.CategoryId == category.Id))
            {
                throw new ArgumentException("The category is already in collection", nameof(category));
            }

            if (isMain && this.ProductCategories.Any(c => c.IsMain))
            {
                throw new ArgumentException("There's already a main category", nameof(category));
            }

            this.ProductCategories.Add(new ProductCategory
            {
                CategoryId = category.Id,
                IsMain = isMain
            });
        }

        /// <summary>
        /// Add the specified category as main category for the product
        /// </summary>
        /// <param name="category">The category to add</param>
        public virtual void AddMainCategory(Category category)
        {
            AddCategory(category, true);
        }

        /// <summary>
        /// Add a product variant
        /// </summary>
        /// <param name="name">The variant name</param>
        /// <param name="ean">The EAN code</param>
        /// <param name="sku">The SKU code</param>
        /// <param name="price">The variant price</param>
        public virtual void AddVariant(string name, string ean, string sku, Currency price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(ean))
            {
                throw new ArgumentNullException(nameof(ean));
            }

            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentNullException(nameof(sku));
            }

            if (price == null)
            {
                throw new ArgumentNullException(nameof(price));
            }

            if (price.Amount < 0)
            {
                throw new ArgumentException("Price amount cannot be less than zero", nameof(price));
            }

            if (this.Variants.Any(v => v.Name == name && v.EanCode == ean && v.Sku == sku))
            {
                throw new InvalidOperationException("The variant is already in collection");
            }

            this.Variants.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                EanCode = ean,
                Sku = sku,
                Price = price
            });
        }

        /// <summary>
        /// Add a tier price to the product
        /// </summary>
        /// <param name="fromQuantity">The start quantity</param>
        /// <param name="toQuantity">The end quantity</param>
        /// <param name="price">The price amount</param>
        public virtual void AddTierPrice(int fromQuantity, int toQuantity, Currency price)
        {
            if (!TierPriceEnabled)
            {
                throw new InvalidOperationException("Tier prices not enabled");
            }

            if (price == null)
            {
                throw new ArgumentNullException(nameof(price));
            }

            if (this.TierPrices.Any(t => t.FromQuantity == fromQuantity && t.ToQuantity == toQuantity))
            {
                throw new ArgumentException("The tier price is already in collection");
            }

            this.TierPrices.Add(new TierPrice
            {
                Id = Guid.NewGuid(),
                FromQuantity = fromQuantity,
                ToQuantity = toQuantity,
                Price = price
            });
        }

        /// <summary>
        /// Add a new custom attribute to the product
        /// </summary>
        /// <param name="attribute">The attribute to add</param>
        /// <param name="value">The attribute's value</param>
        public virtual void AddAttribute(CustomAttribute attribute, object value)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            if (this.Attributes.Any(a => a.Attribute == attribute))
            {
                throw new ArgumentException("The attribute is already in collection", nameof(attribute));
            }

            this.Attributes.Add(new ProductAttribute
            {
                Id = Guid.NewGuid(),
                Attribute = attribute,
                Value = value
            });
        }

        /// <summary>
        /// Add a product review
        /// </summary>
        /// <param name="name">The name of the user who add the review</param>
        /// <param name="rating">The rating given to the product</param>
        public virtual void AddReview(string name, int rating)
        {
            AddReview(name, rating, null);
        }

        /// <summary>
        /// Add a product review
        /// </summary>
        /// <param name="name">The name of the user who add the review</param>
        /// <param name="rating">The rating given to the product</param>
        /// <param name="comment">The comment given to the product</param>
        public virtual void AddReview(string name, int rating, string comment)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (rating < 0)
            {
                throw new ArgumentException("rating could not be less than zero", nameof(rating));
            }

            this.Reviews.Add(new ProductReview
            {
                Id = Guid.NewGuid(),
                Name = name,
                Rating = rating,
                Comment = comment,
                Approved = false,
                CreationDate = DateTime.Now
            });
        }

        /// <summary>
        /// Add an image to the product
        /// </summary>
        /// <param name="path">The image full path</param>
        /// <param name="name">The image's name (no extension)</param>
        /// <param name="originalName">The image's original name</param>
        /// <param name="isMain">Whether the image is the product's main image</param>
        /// <param name="uploadedOn">The date and time of when the image is uploaded</param>
        public virtual void AddImage(string path, string name, string originalName, bool isMain, DateTime uploadedOn)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(originalName))
            {
                throw new ArgumentNullException(nameof(originalName));
            }

            this.Images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                Path = path,
                Name = name,
                OriginalName = originalName,
                IsMain = isMain,
                UploadedOn = uploadedOn
            });
        }

        /// <summary>
        /// Approve the review
        /// </summary>
        /// <param name="reviewId">The id of the review to approve</param>
        public virtual void ApproveReview(Guid reviewId)
        {
            var review = this.Reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review == null)
            {
                throw new InvalidOperationException("Review not found");
            }

            review.Approved = true;
            review.ApprovedOn = DateTime.Now;
        }

        /// <summary>
        /// Remove the approval for the review
        /// </summary>
        /// <param name="reviewId">The id of the review</param>
        public virtual void RemoveReviewApproval(Guid reviewId)
        {
            var review = this.Reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review == null)
            {
                throw new InvalidOperationException("Review not found");
            }

            review.Approved = false;
            review.ApprovedOn = null;
        }

        /// <summary>
        /// Delete the review
        /// </summary>
        /// <param name="reviewId">The id of the review to delete</param>
        public virtual void DeleteReview(Guid reviewId)
        {
            var review = this.Reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review == null)
            {
                throw new InvalidOperationException("Review not found");
            }

            if (!this.Reviews.Remove(review))
            {
                throw new InvalidOperationException("Review not removed");
            }
        }

        /// <summary>
        /// Delete the custom attribute
        /// </summary>
        /// <param name="attributeId">The id of the attribute to delete</param>
        public virtual void DeleteAttribute(Guid attributeId)
        {
            var attribute = this.Attributes.FirstOrDefault(a => a.Id == attributeId);
            if (attribute == null)
            {
                throw new InvalidOperationException("Attribute not found");
            }

            if (!this.Attributes.Remove(attribute))
            {
                throw new InvalidOperationException("Attribute not removed");
            }
        }

        /// <summary>
        /// Delete the image
        /// </summary>
        /// <param name="imageId">The id of the image to delete</param>
        public virtual void DeleteImage(Guid imageId)
        {
            var image = this.Images.FirstOrDefault(i => i.Id == imageId);
            if (image == null)
            {
                throw new InvalidOperationException("Image not found");
            }

            if (!this.Images.Remove(image))
            {
                throw new InvalidOperationException("Image not removed");
            }
        }

        /// <summary>
        /// Delete the tier price
        /// </summary>
        /// <param name="tierPriceId">The id of the tier price to delete</param>
        public virtual void DeleteTierPrice(Guid tierPriceId)
        {
            var tierPrice = this.TierPrices.FirstOrDefault(t => t.Id == tierPriceId);
            if (tierPrice == null)
            {
                throw new InvalidOperationException("Tier price not found");
            }

            if (!this.TierPrices.Remove(tierPrice))
            {
                throw new InvalidOperationException("Tier price not deleted");
            }
        }

        /// <summary>
        /// Remove the product variant
        /// </summary>
        /// <param name="variantId">The id of the variant to remove</param>
        public virtual void RemoveVariant(Guid variantId)
        {
            var variant = this.Variants.FirstOrDefault(v => v.Id == variantId);
            if (variant == null)
            {
                throw new InvalidOperationException("Variant not found");
            }

            if (!this.Variants.Remove(variant))
            {
                throw new InvalidOperationException("Variant not removed");
            }
        }

        /// <summary>
        /// Set the SEO information for the product
        /// </summary>
        /// <param name="seo">The SEO information</param>
        public virtual void SetSeoData(SeoData seo)
        {
            Seo = seo ?? throw new ArgumentNullException(nameof(seo));
        }

        /// <summary>
        /// Change the tier price values
        /// </summary>
        /// <param name="tierPriceId">The tier price id</param>
        /// <param name="fromQuantity">The start quantity</param>
        /// <param name="toQuantity">The end quantity</param>
        /// <param name="price">The price amount</param>
        public virtual void ChangeTierPrice(Guid tierPriceId, int fromQuantity, int toQuantity, Currency price)
        {
            if (!TierPriceEnabled)
            {
                throw new InvalidOperationException("Tier price disabled");
            }

            var tierPrice = this.TierPrices.FirstOrDefault(t => t.Id == tierPriceId);
            if (tierPrice == null)
            {
                throw new InvalidOperationException("Tier price not found");
            }

            if (tierPrice.FromQuantity != fromQuantity)
            {
                tierPrice.FromQuantity = fromQuantity;
            }

            if (tierPrice.ToQuantity != toQuantity)
            {
                tierPrice.ToQuantity = toQuantity;
            }

            if (tierPrice.Price != price)
            {
                tierPrice.Price = price;
            }
        }

        #endregion

        #region Factory Methods
        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="ean">The EAN code</param>
        /// <param name="sku">The SKU code</param>
        /// <param name="name">The product name</param>
        /// <param name="url">The product url</param>
        /// <returns>The created product</returns>
        public static Product Create(string ean, string sku, string name, string url)
        {
            if (string.IsNullOrWhiteSpace(ean))
            {
                throw new ArgumentNullException(nameof(ean));
            }

            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentNullException(nameof(sku));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                EanCode = ean,
                Sku = sku,
                Name = name,
                Url = url,
                Deleted = false,
                CreationDate = DateTime.Now
            };

            return product;
        }

        #endregion
    }
}
