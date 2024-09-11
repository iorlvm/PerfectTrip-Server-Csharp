using PerfectTrip.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Products.Interface
{
    public interface IFacilityRepository
    {
        /// <summary>
        /// 儲存單個 Facility 實體。
        /// </summary>
        /// <param name="facility">要儲存的 Facility 實體。</param>
        /// <returns>儲存更改的行數。</returns>
        Task<int> SaveAsync(Facility facility);

        /// <summary>
        /// 儲存多個 Facility 實體。
        /// </summary>
        /// <param name="facilities">要儲存的 Facility 實體集合。</param>
        /// <returns>儲存更改的行數。</returns>
        Task<int> SaveAllAsync(IEnumerable<Facility> facilities);

        /// <summary>
        /// 獲取所有 Facility 實體。
        /// </summary>
        /// <returns>所有 Facility 實體的列表。</returns>
        Task<List<Facility>> GetAllAsync();

        /// <summary>
        /// 刪除單個 Facility 實體。
        /// </summary>
        /// <param name="facility">要刪除的 Facility 實體。</param>
        /// <returns>刪除更改的行數。</returns>
        Task<int> RemoveAsync(Facility facility);

        /// <summary>
        /// 刪除一組指定的 Facility 實體。
        /// </summary>
        /// <param name="facilities">要刪除的 Facility 實體集合。</param>
        /// <returns>刪除更改的行數。</returns>
        Task<int> RemoveAllAsync(IEnumerable<Facility> facilities);
    }
}
