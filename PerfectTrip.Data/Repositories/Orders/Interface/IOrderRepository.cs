using PerfectTrip.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Orders.Interface
{
    public interface IOrderRepository
    {
        int Save(Order order);

        int SaveAll(IEnumerable<Order> orders);

        int Remove(Order order);

        Order? FindById(int id);

        IEnumerable<Order> GetAll(int pageNumber = 1, int pageSize = 10, bool isAsc = false);

        IEnumerable<Order> GetByUserId(int userId, int pageNumber = 1, int pageSize = 10, bool isAsc = false);

        IEnumerable<Order> GetByCompanyId(int companyId, int pageNumber = 1, int pageSize = 10, bool isAsc = false);
    }
}
