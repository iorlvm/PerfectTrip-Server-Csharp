using PerfectTrip.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Products.Interface
{
    public interface IProductDetailFacilityRepository
    {
        /// <summary>
        /// 新增單個產品-設施關聯條目。
        /// </summary>
        /// <param name="productDetailFacility">產品-設施關聯條目。</param>
        /// <returns>影響的行數。</returns>
        Task<int> SaveAsync(ProductDetailFacility productDetailFacility);

        /// <summary>
        /// 批量新增產品-設施關聯條目。
        /// </summary>
        /// <param name="productDetailFacilities">產品-設施關聯條目集合。</param>
        /// <returns>影響的行數。</returns>
        Task<int> SaveAllAsync(IEnumerable<ProductDetailFacility> productDetailFacilities);

        /// <summary>
        /// 刪除單個產品-設施關聯條目。
        /// </summary>
        /// <param name="productDetailFacility">產品-設施關聯條目。</param>
        /// <returns>影響的行數。</returns>
        Task<int> RemoveAsync(ProductDetailFacility productDetailFacility);

        /// <summary>
        /// 根據產品 ID 刪除所有相關的設施關聯。
        /// </summary>
        /// <param name="productId">產品 ID。</param>
        /// <returns>影響的行數。</returns>
        Task<int> RemoveByProductIdAsync(int productId);

        /// <summary>
        /// 根據設施 ID 刪除所有相關的產品關聯。
        /// </summary>
        /// <param name="facilityId">設施 ID。</param>
        /// <returns>影響的行數。</returns>
        Task<int> RemoveByFacilityIdAsync(int facilityId);
    }
}
