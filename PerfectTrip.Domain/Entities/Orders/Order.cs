using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectTrip.Common.Entities.Member;

namespace PerfectTrip.Domain.Entities.Orders
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        [MaxLength(255)]
        public string PayStatus { get; set; } = "未付款"; // 付款後存放退款碼

        public int? FullPrice { get; set; }

        public int? ServiceFee { get; set; }

        public int? Discount { get; set; }

        public int? Tax { get; set; }

        public int? ActualPrice { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "text")]
        public string? OrderNotes { get; set; }

        public string? WishedTime { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }
    }
}
