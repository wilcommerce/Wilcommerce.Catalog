using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Catalog.ReadModels;
using Xunit;

namespace Wilcommerce.Catalog.Test.ReadModels
{
    public class ProductReviewExtensionsTest
    {
        #region ByProduct tests
        [Fact]
        public void ByProduct_Should_Throw_ArgumentNullException_If_Reviews_Is_Null()
        {
            IQueryable<ProductReview> reviews = null;
            Guid productId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() => ProductReviewExtensions.ByProduct(reviews, productId));
            Assert.Equal(nameof(reviews), ex.ParamName);
        }

        [Fact]
        public void ByProduct_Should_Throw_ArgumentNullException_If_ProductId_Is_Empty()
        {
            IQueryable<ProductReview> reviews = new ProductReview[0].AsQueryable();
            Guid productId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => ProductReviewExtensions.ByProduct(reviews, productId));
            Assert.Equal(nameof(productId), ex.ParamName);
        }
        #endregion

        #region Approved tests
        [Fact]
        public void Approved_Should_Throw_ArgumentNullException_If_Reviews_Is_Null()
        {
            IQueryable<ProductReview> reviews = null;

            var ex = Assert.Throws<ArgumentNullException>(() => ProductReviewExtensions.Approved(reviews));
            Assert.Equal(nameof(reviews), ex.ParamName);
        }

        [Fact]
        public void Approved_Should_Return_Only_Approved_Reviews()
        {
            IQueryable<ProductReview> reviews = new ProductReview[]
            {
                new ProductReview{ Id = Guid.NewGuid(), Approved = true },
                new ProductReview{ Id = Guid.NewGuid(), Approved = true },
                new ProductReview{ Id = Guid.NewGuid(), Approved = false }
            }.AsQueryable();

            var approvedReviews = ProductReviewExtensions.Approved(reviews).ToArray();
            Assert.True(approvedReviews.All(r => r.Approved));
        }
        #endregion
    }
}
