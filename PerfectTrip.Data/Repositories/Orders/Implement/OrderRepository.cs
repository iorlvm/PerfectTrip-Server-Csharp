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

        public Order? FindById(int id)
        {
            return _dbContext.Orders.Find(id);
        }

        public IEnumerable<Order> GetAll(int pageNumber = 1, int pageSize = 10, bool isAsc = false)
        {
            IQueryable<Order> query = _dbContext.Orders;
            if (isAsc) 
            {
                query = query.OrderBy(x => x.OrderId);
            } 
            else 
            { 
                query = query.OrderByDescending(x => x.OrderId);
            }

            var offset = (pageNumber - 1) * pageSize;

            return query.Skip(offset).Take(pageSize).ToList();
        }

        public IEnumerable<Order> GetByCompanyId(int companyId, int pageNumber = 1, int pageSize = 10, bool isAsc = false)
        {
            var query = from o in _dbContext.Orders
                        join od in _dbContext.OrderDetails
                        on o.OrderId equals od.OrderId
                        join p in _dbContext.Products 
                        on od.ProductId equals p.ProductId
                        where p.CompanyId == companyId 
                        orderby isAsc ? o.OrderId : o.OrderId descending
                        select o;

            var offset = (pageNumber - 1) * pageSize;

            return query.Skip(offset).Take(pageSize).ToList();
        }

        public IEnumerable<Order> GetByUserId(int userId, int pageNumber = 1, int pageSize = 10, bool isAsc = false)
        {
            IQueryable<Order> query = _dbContext.Orders;
            if (isAsc)
            {
                query = query.OrderBy(x => x.OrderId);
            }
            else
            {
                query = query.OrderByDescending(x => x.OrderId);
            }
            var offset = (pageNumber - 1) * pageSize;

            return query.Where(x => x.UserId == userId)
                .Skip(offset)
                .Take(pageSize)
                .ToList();
        }

        public int Remove(Order order)
        {
            _dbContext.Remove(order);
            return _dbContext.SaveChanges();
        }

        public int Save(Order order)
        {
            if (order == null) throw new ArgumentNullException("order is null");
            if (order.OrderId <= 0)
            {
                _dbContext.Orders.Add(order);
            }
            else 
            {
                _dbContext.Orders.Update(order);
            }
            return _dbContext.SaveChanges();
        }

        public int SaveAll(IEnumerable<Order> orders)
        {
            if (orders == null || !orders.Any())
            {
                throw new ArgumentException("orders is null or empty");
            }

            foreach (var order in orders)
            {
                if (order.OrderId <= 0)
                {
                    _dbContext.Orders.Add(order);
                }
                else
                {
                    _dbContext.Orders.Update(order);
                }
            }

            return _dbContext.SaveChanges();
        }
    }
}
