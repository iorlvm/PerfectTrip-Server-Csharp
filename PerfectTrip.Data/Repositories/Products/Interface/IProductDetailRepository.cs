using PerfectTrip.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Products.Interface
{
    public interface IProductDetailRepository
    {
        /// <summary>
        /// 根據產品 ID 獲取產品詳細資訊及其關聯的產品和設施。
        /// </summary>
        /// <param name="productId">產品 ID。</param>
        /// <returns>包含關聯產品及設施的產品詳細資訊。</returns>
        public Task<ProductDetail> GetWithProductAndFacilitiesAsync(int productId);
    }
}
