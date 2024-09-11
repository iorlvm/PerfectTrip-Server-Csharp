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
        /// <summary>
        /// 儲存單個 Order 實體。
        /// </summary>
        /// <param name="order">要儲存的 Order 實體。</param>
        /// <returns>受影響的行數（通常為 1）。</returns>
        Task<int> SaveAsync(Order order);

        /// <summary>
        /// 儲存多個 Order 實體。
        /// </summary>
        /// <param name="orders">要儲存的 Order 實體集合。</param>
        /// <returns>受影響的行數。</returns>
        Task<int> SaveAllAsync(IEnumerable<Order> orders);

        /// <summary>
        /// 刪除單個 Order 實體。
        /// </summary>
        /// <param name="order">要刪除的 Order 實體。</param>
        /// <returns>受影響的行數（通常為 1）。</returns>
        Task<int> RemoveAsync(Order order);

        /// <summary>
        /// 根據 ID 查找 Order 實體。
        /// </summary>
        /// <param name="id">要查找的 Order ID。</param>
        /// <returns>找到的 Order 實體，如果不存在則為 null。</returns>
        Task<Order?> FindByIdAsync(int id);

        /// <summary>
        /// 查詢所有 Order 實體，支持分頁和排序。
        /// </summary>
        /// <param name="pageNumber">頁碼（從 1 開始）。</param>
        /// <param name="pageSize">每頁顯示的項目數。</param>
        /// <param name="isAsc">是否按升序排序。</param>
        /// <returns>包含分頁資訊的 Order 集合。</returns>
        Task<Page<Order>> GetAllAsync(int pageNumber = 1, int pageSize = 10, bool isAsc = false);

        /// <summary>
        /// 根據 User ID 查詢 Order 實體，支持分頁和排序。
        /// </summary>
        /// <param name="userId">要查詢的 User ID。</param>
        /// <param name="pageNumber">頁碼（從 1 開始）。</param>
        /// <param name="pageSize">每頁顯示的項目數。</param>
        /// <param name="isAsc">是否按升序排序。</param>
        /// <returns>包含分頁資訊的 Order 集合。</returns>
        Task<Page<Order>> GetByUserIdAsync(int userId, int pageNumber = 1, int pageSize = 10, bool isAsc = false);

        /// <summary>
        /// 根據 Company ID 查詢 Order 實體，支持分頁和排序。
        /// </summary>
        /// <param name="companyId">要查詢的 Company ID。</param>
        /// <param name="pageNumber">頁碼（從 1 開始）。</param>
        /// <param name="pageSize">每頁顯示的項目數。</param>
        /// <param name="isAsc">是否按升序排序。</param>
        /// <returns>包含分頁資訊的 Order 集合。</returns>
        Task<Page<Order>> GetByCompanyIdAsync(int companyId, int pageNumber = 1, int pageSize = 10, bool isAsc = false);

        /// <summary>
        /// 根據查詢條件獲取分頁的 Order 實體集合。
        /// </summary>
        /// <param name="query">需要進行分頁的查詢。</param>
        /// <param name="pageNumber">頁碼（從 1 開始）。</param>
        /// <param name="pageSize">每頁顯示的項目數。</param>
        /// <param name="isAsc">是否按升序排序。</param>
        /// <returns>包含分頁資訊的 Order 集合。</returns>
        Task<Page<Order>> GetPagedOrdersAsync(IQueryable<Order> query, int pageNumber, int pageSize, bool isAsc);
    }
}
