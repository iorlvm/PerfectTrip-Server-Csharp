using Microsoft.EntityFrameworkCore;
using PerfectTrip.Data.Repositories.Orders.Interface;
using PerfectTrip.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Orders.Implement
{
    public class OrderResidentRepository : IOrderResidentRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public OrderResidentRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderResident>> GetByOrderIdAsync(int orderId)
        {
            return await _dbContext.OrderResidents
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<int> RemoveAsync(OrderResident orderResident)
        {
            _dbContext.OrderResidents.Remove(orderResident);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveByOrderIdAsync(int orderId)
        {
            var orderResidents = await _dbContext.OrderResidents
                .Where(x => x.OrderId == orderId)
                .ToListAsync();

            if (!orderResidents.Any())
            {
                return 0;
            }

            _dbContext.OrderResidents.RemoveRange(orderResidents);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAllAsync(IEnumerable<OrderResident> orderResidents)
        {
            if (orderResidents == null || !orderResidents.Any())
            {
                throw new ArgumentException("orderResidents is null or empty");
            }

            var newOrderResidents = new List<OrderResident>();
            foreach (var orderResident in orderResidents)
            {
                if (orderResident.Id <= 0)
                {
                    newOrderResidents.Add(orderResident);
                }
                else
                {
                    _dbContext.OrderResidents.Update(orderResident);
                }
            }

            if (newOrderResidents.Any())
            {
                await _dbContext.OrderResidents.AddRangeAsync(newOrderResidents);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(OrderResident orderResident)
        {
            if (orderResident == null) throw new ArgumentNullException(nameof(orderResident));

            if (orderResident.Id <= 0) 
            {
                await _dbContext.OrderResidents.AddAsync(orderResident);
            }
            else
            {
                _dbContext.OrderResidents.Update(orderResident);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
