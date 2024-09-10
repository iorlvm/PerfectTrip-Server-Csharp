using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using PerfectTrip.Common.Enums;

namespace PerfectTrip.Common.Entities.Member
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        [MaxLength(255)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(255)]
        public string VatNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(20)]
        public string Telephone { get; set; }

        [Required]
        public float Score { get; set; } = 8.0F;

        [Required]
        public PassStatus Pass { get; set; } = PassStatus.Pending;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
