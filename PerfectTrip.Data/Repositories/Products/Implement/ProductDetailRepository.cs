using Microsoft.EntityFrameworkCore;
using PerfectTrip.Data.Repositories.Products.Interface;
using PerfectTrip.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Products.Implement
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public ProductDetailRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> RemoveByProductIdAsync(int productId)
        {
            var productDetail = await _dbContext.ProductDetails
                .FirstOrDefaultAsync(pd => pd.ProductId == productId);

            if (productDetail == null) return 0;

            _dbContext.ProductDetails.Remove(productDetail);
            return _dbContext.SaveChanges();
        }

        public async Task<ProductDetail> GetWithProductAndFacilitiesAsync(int productId)
        {
            return await _dbContext.ProductDetails
                .Include(pd => pd.Product)
                .Include(pd => pd.Facilities)
                .FirstOrDefaultAsync(pd => pd.ProductId == productId);
        }

        public async Task<int> SaveAsync(ProductDetail productDetail)
        {
            if (productDetail == null) throw new ArgumentNullException(nameof(productDetail));

            if (productDetail.ProductDetailId <= 0)
            {
                await _dbContext.ProductDetails.AddAsync(productDetail);
            }
            else
            {
                _dbContext.ProductDetails.Update(productDetail);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
