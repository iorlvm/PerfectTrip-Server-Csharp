using PerfectTrip.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Data.Interface
{
    public interface IImageRepository
    {
        /// <summary>
        /// 儲存單張圖片實體。如果圖片已存在則進行更新，否則進行新增。
        /// </summary>
        /// <param name="image">要儲存的圖片實體。</param>
        /// <returns>受影響的資料行數（通常為 1）。</returns>
        Task<int> SaveAsync(Image image);

        /// <summary>
        /// 儲存多張圖片實體。如果圖片已存在則進行更新，否則進行新增。
        /// </summary>
        /// <param name="images">要儲存的圖片實體集合。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> SaveAllAsync(IEnumerable<Image> images);

        /// <summary>
        /// 刪除單張圖片實體。
        /// </summary>
        /// <param name="image">要刪除的圖片實體。</param>
        /// <returns>受影響的資料行數（通常為 1）。</returns>
        Task<int> RemoveAsync(Image image);

        /// <summary>
        /// 根據圖片 ID 查找圖片實體。
        /// </summary>
        /// <param name="id">圖片 ID。</param>
        /// <returns>找到的圖片實體，如果不存在則為 null。</returns>
        Task<Image?> FindByIdAsync(int id);

        /// <summary>
        /// 根據圖片 ID 刪除圖片實體。
        /// </summary>
        /// <param name="id">圖片 ID。</param>
        /// <returns>受影響的資料行數（通常為 1）。</returns>
        Task<int> RemoveByIdAsync(int id);

        /// <summary>
        /// 根據一組圖片 ID 刪除多張圖片實體。
        /// </summary>
        /// <param name="ids">圖片 ID 列表。</param>
        /// <returns>受影響的資料行數。</returns>
        Task<int> RemoveAllAsync(IEnumerable<int> ids);
    }
}
