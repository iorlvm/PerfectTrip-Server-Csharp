using PerfectTrip.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Products.Interface
{
    public interface IProductDiscountRepository
    {
        /// <summary>
        /// 根據公司 ID 獲取該公司所有的產品折扣資訊。
        /// </summary>
        /// <param name="companyId">公司 ID。</param>
        /// <returns>包含所有產品折扣資訊的集合。</returns>
        Task<List<ProductDiscount>> GetByCompanyIdAsync(int companyId);

        /// <summary>
        /// 儲存產品折扣資訊。如果產品折扣已存在則進行更新，否則進行新增。
        /// </summary>
        /// <param name="productDiscount">要儲存的產品折扣資訊物件。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> SaveAsync(ProductDiscount productDiscount);

        /// <summary>
        /// 刪除指定的產品折扣資訊。
        /// </summary>
        /// <param name="productDiscount">要刪除的產品折扣資訊物件。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> RemoveAsync(ProductDiscount productDiscount);
    }
}
