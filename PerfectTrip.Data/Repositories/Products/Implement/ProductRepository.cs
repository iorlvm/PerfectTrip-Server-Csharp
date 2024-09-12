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
    public class ProductRepository : IProductRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public ProductRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> RemoveAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveByCompanyIdAsync(int companyId)
        {
            var products = await _dbContext.Products
                .Where(p => p.CompanyId == companyId)
                .ToListAsync();

            if (!products.Any())
            {
                return 0;
            }

            _dbContext.Products.RemoveRange(products);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveAllAsync(IEnumerable<int> productIds)
        {
            if (productIds == null || !productIds.Any())
            {
                return 0;
            }

            var products = await _dbContext.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();

            if (!products.Any())
            {
                return 0;
            }

            _dbContext.Products.RemoveRange(products);
            return await _dbContext.SaveChangesAsync();
        }


        public async Task<int> SaveAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            if (product.ProductId <= 0)
            {
                await _dbContext.Products.AddAsync(product);
            }
            else 
            { 
                _dbContext.Products.Update(product);
            }

            return await _dbContext.SaveChangesAsync();

        }

        public async Task<int> SaveAllAsync(IEnumerable<Product> products)
        {
            if (products == null || !products.Any())
            {
                throw new ArgumentException("products is null or empty");
            }

            var newProducts = new List<Product>();
            foreach (var product in products)
            {
                if (product.ProductId <= 0)
                {
                    newProducts.Add(product);
                }
                else
                {
                    _dbContext.Products.Update(product);
                }
            }

            if (newProducts.Any())
            {
                await _dbContext.Products.AddRangeAsync(newProducts);
            }

            return await _dbContext.SaveChangesAsync();
        }

    }
}
