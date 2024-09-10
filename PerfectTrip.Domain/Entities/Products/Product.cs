using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectTrip.Common.Entities.Member;

namespace PerfectTrip.Domain.Entities.Products
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int MaxOccupancy { get; set; } = 2;

        [Required]
        [MaxLength(255)]
        public string ProductName { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }
    }
}
