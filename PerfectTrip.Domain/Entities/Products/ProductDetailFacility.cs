using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Domain.Entities.Products
{
    public class ProductDetailFacility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProductDetailId { get; set; }

        [ForeignKey("ProductDetailId")]
        public virtual ProductDetail ProductDetail { get; set; }

        [Required]
        public int FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility Facility { get; set; }
    }
}
