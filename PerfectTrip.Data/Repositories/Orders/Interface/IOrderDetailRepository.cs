using PerfectTrip.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Orders.Interface
{
    public interface IOrderDetailRepository
    {
        /// <summary>
        /// 儲存單個 OrderDetail 實體。
        /// </summary>
        /// <param name="orderDetail">要儲存的 OrderDetail 實體。</param>
        /// <returns>受影響的行數。</returns>
        Task<int> SaveAsync(OrderDetail orderDetail, DateTime expiredTime);

        /// <summary>
        /// 儲存多個 OrderDetail 實體。
        /// </summary>
        /// <param name="orderDetails">要儲存的 OrderDetail 實體集合。</param>
        /// <returns>受影響的行數。</returns>
        Task<int> SaveAllAsync(IEnumerable<OrderDetail> orderDetails, DateTime expiredTime);

        /// <summary>
        /// 刪除單個 OrderDetail 實體。
        /// </summary>
        /// <param name="orderDetail">要刪除的 OrderDetail 實體。</param>
        /// <returns>受影響的行數。</returns>
        Task<int> RemoveAsync(OrderDetail orderDetail);

        /// <summary>
        /// 根據 OrderId 刪除所有相關的 OrderDetail 實體。
        /// </summary>
        /// <param name="orderId">要刪除的 OrderId。</param>
        /// <returns>受影響的行數。</returns>
        Task<int> RemoveByOrderIdAsync(int orderId);

        /// <summary>
        /// 根據 OrderId 查詢所有相關的 OrderDetail 實體。
        /// </summary>
        /// <param name="orderId">要查詢的 OrderId。</param>
        /// <returns>符合條件的 OrderDetail 實體集合。</returns>
        Task<List<OrderDetail>> GetByOrderIdAsync(int orderId);
    }
}
