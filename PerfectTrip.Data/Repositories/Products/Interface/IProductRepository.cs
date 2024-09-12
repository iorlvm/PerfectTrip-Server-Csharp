using PerfectTrip.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Products.Interface
{
    public interface IProductRepository
    {
        /// <summary>
        /// 儲存單個產品資訊。如果產品已存在則進行更新，否則進行新增。
        /// </summary>
        /// <param name="product">要儲存的產品資訊。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> SaveAsync(Product product);

        /// <summary>
        /// 儲存多個產品資訊。如果產品已存在則進行更新，否則進行新增。
        /// </summary>
        /// <param name="products">要儲存的產品資訊集合。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> SaveAllAsync(IEnumerable<Product> products);

        /// <summary>
        /// 刪除單個產品資訊。
        /// </summary>
        /// <param name="product">要刪除的商品實體。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> RemoveAsync(Product product);

        /// <summary>
        /// 根據公司 ID 刪除多個產品資訊。
        /// </summary>
        /// <param name="companyId">公司 ID。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> RemoveByCompanyIdAsync(int companyId);

        /// <summary>
        /// 根據傳入的產品 ID 列表刪除多個產品資訊。
        /// </summary>
        /// <param name="productIds">產品 ID 列表。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> RemoveAllAsync(IEnumerable<int> productIds);
    }

}
