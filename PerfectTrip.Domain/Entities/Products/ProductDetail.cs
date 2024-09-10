using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PerfectTrip.Domain.Entities.Products
{
    public class ProductDetail
    {
        [Key]
        public int ProductDetailId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        
        public bool IncludesBreakfast { get; set; } = false;

        public bool AllowDateChanges { get; set; } = false;
        
        public bool IsRefundable { get; set; } = false;
        
        public bool AllowFreeCancellation { get; set; } = false;

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
