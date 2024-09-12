using PerfectTrip.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Products.Interface
{
    public interface IProductPhotoRepository
    {
        /// <summary>
        /// 根據商品 ID 獲取該商品的圖片列表。
        /// </summary>
        /// <param name="productId">商品 ID。</param>
        /// <returns>包含所有圖片的集合。</returns>
        Task<List<ProductPhoto>> GetByProductIdAsync(int productId);

        /// <summary>
        /// 儲存單張產品圖片。
        /// </summary>
        /// <param name="productPhoto">要儲存的產品圖片。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> SaveAsync(ProductPhoto productPhoto);

        /// <summary>
        /// 儲存多張產品圖片。
        /// </summary>
        /// <param name="productPhotos">要儲存的產品圖片集合。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> SaveAllAsync(IEnumerable<ProductPhoto> productPhotos);

        /// <summary>
        /// 刪除單張產品圖片。
        /// </summary>
        /// <param name="photo">要刪除的相片實體。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> RemoveAsync(ProductPhoto productPhoto);

        /// <summary>
        /// 根據商品 ID 刪除該商品的所有圖片。
        /// </summary>
        /// <param name="productId">商品 ID。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> RemoveByProductIdAsync(int productId);
    }

}
