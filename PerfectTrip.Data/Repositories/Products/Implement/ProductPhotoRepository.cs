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
    public class ProductPhotoRepository : IProductPhotoRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public ProductPhotoRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<ProductPhoto>> GetByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> RemoveAsync(ProductPhoto productPhoto)
        {
            _dbContext.ProductPhotos.Remove(productPhoto);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveByProductIdAsync(int productId)
        {
            var productPhotos = await _dbContext.ProductPhotos
                .Where(p => p.ProductId == productId)
                .ToListAsync();

            if (!productPhotos.Any())
            {
                return 0;
            }

            _dbContext.ProductPhotos.RemoveRange(productPhotos);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAllAsync(IEnumerable<ProductPhoto> productPhotos)
        {
            if (productPhotos == null || !productPhotos.Any())
            {
                throw new ArgumentException("productPhotos is null or empty");
            }

            var newProductPhotos = new List<ProductPhoto>();
            foreach (var productPhoto in productPhotos)
            {
                if (productPhoto.ProductId <= 0)
                {
                    newProductPhotos.Add(productPhoto);
                }
                else
                {
                    _dbContext.ProductPhotos.Update(productPhoto);
                }
            }

            if (newProductPhotos.Any())
            {
                await _dbContext.ProductPhotos.AddRangeAsync(newProductPhotos);
            }

            return await _dbContext.SaveChangesAsync();
        }


        public async Task<int> SaveAsync(ProductPhoto productPhoto)
        {
            if (productPhoto == null) throw new ArgumentNullException(nameof(productPhoto));

            if (productPhoto.ProductId <= 0)
            {
                await _dbContext.ProductPhotos.AddAsync(productPhoto);
            }
            else
            {
                _dbContext.ProductPhotos.Update(productPhoto);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
