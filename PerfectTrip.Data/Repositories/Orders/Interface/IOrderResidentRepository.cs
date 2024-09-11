using PerfectTrip.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Orders.Interface
{
    public interface IOrderResidentRepository
    {
        /// <summary>
        /// 保存單個 OrderResident 實體。
        /// </summary>
        /// <param name="orderResident">要保存的 OrderResident 實體。</param>
        /// <returns>保存更改的行數。</returns>
        Task<int> SaveAsync(OrderResident orderResident);

        /// <summary>
        /// 保存多個 OrderResident 實體。
        /// </summary>
        /// <param name="orderResidents">要保存的 OrderResident 實體集合。</param>
        /// <returns>保存更改的行數。</returns>
        Task<int> SaveAllAsync(IEnumerable<OrderResident> orderResidents);

        /// <summary>
        /// 刪除單個 OrderResident 實體。
        /// </summary>
        /// <param name="orderResident">要刪除的 OrderResident 實體。</param>
        /// <returns>刪除更改的行數。</returns>
        Task<int> RemoveAsync(OrderResident orderResident);

        /// <summary>
        /// 根據 OrderId 刪除所有相關的 OrderResident 實體。
        /// </summary>
        /// <param name="orderId">要刪除的 OrderId。</param>
        /// <returns>受影響的行數。</returns>
        Task<int> RemoveByOrderIdAsync(int orderId);

        /// <summary>
        /// 根據 Order ID 獲取 OrderResident 實體列表。
        /// </summary>
        /// <param name="orderId">Order 的 ID。</param>
        /// <returns>OrderResident 實體列表。</returns>
        Task<List<OrderResident>> GetByOrderIdAsync(int orderId);
    }
}
