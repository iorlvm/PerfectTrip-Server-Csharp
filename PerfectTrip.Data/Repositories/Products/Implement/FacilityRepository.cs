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
    public class FacilityRepository : IFacilityRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public FacilityRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Facility>> GetAllAsync()
        {
            return await _dbContext.Facilities.ToListAsync();
        }

        public async Task<int> RemoveAllAsync(IEnumerable<int> facilityIds)
        {
            if (facilityIds == null || !facilityIds.Any())
            {
                return 0;
            }

            var facilities = await _dbContext.Facilities
                .Where(f => facilityIds.Contains(f.FacilityId))
                .ToListAsync();

            if (!facilities.Any())
            {
                return 0;
            }

            _dbContext.Facilities.RemoveRange(facilities);

            return await _dbContext.SaveChangesAsync();
        }


        public async Task<int> RemoveAsync(Facility facility)
        {
            _dbContext.Facilities.Remove(facility);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAllAsync(IEnumerable<Facility> facilities)
        {
            if (facilities == null || !facilities.Any())
            {
                throw new ArgumentException("facilities is null or empty");
            }

            var newFacilities = new List<Facility>();
            foreach (var facility in facilities)
            {
                if (facility.FacilityId <= 0)
                {
                    newFacilities.Add(facility);
                }
                else
                {
                    _dbContext.Facilities.Update(facility);
                }
            }
            if (newFacilities.Any())
            {
                await _dbContext.Facilities.AddRangeAsync(newFacilities);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(Facility facility)
        {
            if (facility == null) throw new ArgumentNullException(nameof(facility));

            if (facility.FacilityId <= 0)
            {
                await _dbContext.Facilities.AddAsync(facility);
            }
            else
            {
                _dbContext.Facilities.Update(facility);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
