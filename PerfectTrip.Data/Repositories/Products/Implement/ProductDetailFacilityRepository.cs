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
    public class ProductDetailFacilityRepository : IProductDetailFacilityRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public ProductDetailFacilityRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> RemoveAsync(ProductDetailFacility productDetailFacility)
        {
            _dbContext.ProductDetailFacilities.Remove(productDetailFacility);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveByFacilityIdAsync(int facilityId)
        {
            var productDetailFacilities = await _dbContext.ProductDetailFacilities
                .Where(p => p.FacilityId == facilityId)
                .ToListAsync();

            if (!productDetailFacilities.Any()) 
            {
                return 0;
            }

            _dbContext.ProductDetailFacilities.RemoveRange(productDetailFacilities);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveByProductIdAsync(int productId)
        {
            var query = from pdf in _dbContext.ProductDetailFacilities
                        join pd in _dbContext.ProductDetails
                        on pdf.ProductDetailId equals pd.ProductDetailId
                        where pd.ProductId == productId
                        select pdf;

            var productDetailFacilities = await query.Distinct().ToListAsync();

            if (!productDetailFacilities.Any())
            {
                return 0;
            }

            _dbContext.ProductDetailFacilities.RemoveRange(productDetailFacilities);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAllAsync(IEnumerable<ProductDetailFacility> productDetailFacilities)
        {
            if (productDetailFacilities == null || !productDetailFacilities.Any())
            {
                throw new ArgumentException("productDetailFacilities is null or empty");
            }

            var newProductDetailFacilities = new List<ProductDetailFacility>();
            foreach (var productDetailFacility in productDetailFacilities)
            {
                if (productDetailFacility.Id <= 0)
                {
                    newProductDetailFacilities.Add(productDetailFacility);
                }
                else
                {
                    _dbContext.ProductDetailFacilities.Update(productDetailFacility);
                }
            }

            if (newProductDetailFacilities.Any())
            {
                await _dbContext.ProductDetailFacilities.AddRangeAsync(newProductDetailFacilities);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(ProductDetailFacility productDetailFacility)
        {
            if (productDetailFacility == null) throw new ArgumentNullException(nameof(productDetailFacility));

            if (productDetailFacility.Id <= 0)
            {
                await _dbContext.ProductDetailFacilities.AddAsync(productDetailFacility);
            }
            else
            {
                _dbContext.ProductDetailFacilities.Update(productDetailFacility);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
