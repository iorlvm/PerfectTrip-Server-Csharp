using Microsoft.EntityFrameworkCore;
using PerfectTrip.Data.Repositories.Data.Interface;
using PerfectTrip.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Data.Implement
{
    public class ImageRepository : IImageRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public ImageRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveAsync(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (image.Id <= 0)
            {
                await _dbContext.Set<Image>().AddAsync(image);
            }
            else
            {
                _dbContext.Set<Image>().Update(image);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAllAsync(IEnumerable<Image> images)
        {
            if (images == null || !images.Any())
            {
                throw new ArgumentException("images is null or empty");
            }

            var newImages = new List<Image>();
            foreach (var image in images)
            {
                if (image.Id <= 0)
                {
                    newImages.Add(image);
                }
                else
                {
                    _dbContext.Set<Image>().Update(image);
                }
            }

            if (newImages.Any())
            {
                await _dbContext.Set<Image>().AddRangeAsync(newImages);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            _dbContext.Set<Image>().Remove(image);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Image?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Image>().FindAsync(id);
        }

        public async Task<int> RemoveByIdAsync(int id)
        {
            var image = await FindByIdAsync(id);
            if (image == null)
            {
                return 0;
            }

            _dbContext.Set<Image>().Remove(image);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveAllAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return 0;
            }

            var images = await _dbContext.Set<Image>()
                .Where(img => ids.Contains(img.Id))
                .ToListAsync();

            if (!images.Any())
            {
                return 0;
            }

            _dbContext.Set<Image>().RemoveRange(images);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
