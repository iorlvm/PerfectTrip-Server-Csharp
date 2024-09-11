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
    public class OrderRepository : IOrderRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public OrderRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order?> FindByIdAsync(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task<Page<Order>> GetAllAsync(int pageNumber = 1, int pageSize = 10, bool isAsc = false)
        {
            IQueryable<Order> query = _dbContext.Orders;

            return await GetPagedOrdersAsync(query, pageNumber, pageSize, isAsc);
        }

        public async Task<Page<Order>> GetByCompanyIdAsync(int companyId, int pageNumber = 1, int pageSize = 10, bool isAsc = false)
        {
            var query = from o in _dbContext.Orders
                        join od in _dbContext.OrderDetails on o.OrderId equals od.OrderId
                        join p in _dbContext.Products on od.ProductId equals p.ProductId
                        where p.CompanyId == companyId
                        select o;

            return await GetPagedOrdersAsync(query.Distinct(), pageNumber, pageSize, isAsc);
        }

        public async Task<Page<Order>> GetByUserIdAsync(int userId, int pageNumber = 1, int pageSize = 10, bool isAsc = false)
        {
            IQueryable<Order> query = _dbContext.Orders.Where(o => o.UserId == userId);

            return await GetPagedOrdersAsync(query, pageNumber, pageSize, isAsc);
        }

        public async Task<Page<Order>> GetPagedOrdersAsync(IQueryable<Order> query, int pageNumber, int pageSize, bool isAsc)
        {
            var totalItems = await query.CountAsync();

            query = isAsc ? query.OrderBy(x => x.OrderId) : query.OrderByDescending(x => x.OrderId);

            var offset = (pageNumber - 1) * pageSize;
            var items = await query.Skip(offset).Take(pageSize).ToListAsync();

            return new Page<Order>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<int> RemoveAsync(Order order)
        {
            _dbContext.Orders.Remove(order);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            var now = DateTime.Now;

            if (order.OrderId <= 0)
            {
                order.CreatedDate = now;
                order.LastModifiedDate = now;
                await _dbContext.Orders.AddAsync(order);
            }
            else
            {
                order.LastModifiedDate = now;
                _dbContext.Orders.Update(order);
            }

            return await _dbContext.SaveChangesAsync();
        }


        public async Task<int> SaveAllAsync(IEnumerable<Order> orders)
        {
            if (orders == null || !orders.Any())
            {
                throw new ArgumentException("orders is null or empty");
            }

            var now = DateTime.Now;
            var newOrders = new List<Order>();
            foreach (var order in orders)
            {
                if (order.OrderId <= 0)
                {
                    order.CreatedDate = now;
                    order.LastModifiedDate = now;
                    newOrders.Add(order);
                }
                else
                {
                    order.LastModifiedDate = now;
                    _dbContext.Orders.Update(order);
                }
            }

            if (newOrders.Any())
            {
                await _dbContext.Orders.AddRangeAsync(newOrders);
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
