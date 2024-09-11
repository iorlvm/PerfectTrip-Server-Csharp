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

        public async Task<ProductDetail> GetWithProductAndFacilitiesAsync(int productId)
        {
            return await _dbContext.ProductDetails
                .Include(pd => pd.Product)
                .Include(pd => pd.Facilities)
                .FirstOrDefaultAsync(pd => pd.ProductId == productId);
        }
    }
}
