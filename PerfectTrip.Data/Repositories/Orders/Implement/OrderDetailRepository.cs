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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public OrderDetailRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderDetail>> GetByOrderIdAsync(int orderId)
        {
            return await _dbContext.OrderDetails
                .Where(od => od.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<int> RemoveAsync(OrderDetail orderDetail)
        {
            _dbContext.OrderDetails.Remove(orderDetail);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveByOrderIdAsync(int orderId)
        {
            var orderDetails = await _dbContext.OrderDetails
                .Where(od => od.OrderId == orderId)
                .ToListAsync();

            if (!orderDetails.Any())
            {
                return 0;
            }

            _dbContext.OrderDetails.RemoveRange(orderDetails);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAllAsync(IEnumerable<OrderDetail> orderDetails, DateTime expiredTime)
        {
            if (orderDetails == null || !orderDetails.Any())
            {
                throw new ArgumentException("orderDetails is null or empty");
            }

            var newOrderDetails = new List<OrderDetail>();
            foreach (var orderDetail in orderDetails) {
                orderDetail.ExpiredTime = expiredTime;

                if (orderDetail.Id <= 0)
                {
                    newOrderDetails.Add(orderDetail);
                }
                else
                {
                    _dbContext.OrderDetails.Update(orderDetail);
                }
            }

            if (newOrderDetails.Any())
            {
                await _dbContext.OrderDetails.AddRangeAsync(newOrderDetails);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(OrderDetail orderDetail, DateTime expiredTime)
        {
            if (orderDetail == null) throw new ArgumentNullException(nameof(orderDetail));

            orderDetail.ExpiredTime = expiredTime;

            if (orderDetail.Id <= 0)
            {
                await _dbContext.OrderDetails.AddAsync(orderDetail);
            }
            else
            {
                _dbContext.OrderDetails.Update(orderDetail);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
