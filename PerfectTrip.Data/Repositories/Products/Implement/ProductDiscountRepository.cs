using PerfectTrip.Data.Repositories.Products.Interface;
using PerfectTrip.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Products.Implement
{
    public class ProductDiscountRepository : IProductDiscountRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public ProductDiscountRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<ProductDiscount>> GetByCompanyIdAsync(int companyId)
        {
            
            throw new NotImplementedException();
        }

        public async Task<int> RemoveAsync(ProductDiscount productDiscount)
        {
            _dbContext.ProductDiscounts.Remove(productDiscount);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(ProductDiscount productDiscount)
        {
            if (productDiscount == null) throw new ArgumentNullException(nameof(productDiscount));

            if (productDiscount.ProductDiscountId <= 0)
            {
                await _dbContext.ProductDiscounts.AddAsync(productDiscount);
            } 
            else
            {
                _dbContext.ProductDiscounts.Update(productDiscount);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
