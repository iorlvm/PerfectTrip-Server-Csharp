using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectTrip.Common.Enums;

namespace PerfectTrip.Common.Entities.Member
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public AdminGroup AdminGroup { get; set; } = AdminGroup.Viewer;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        public void UpdateLastModifiedDate()
        {
            LastModifiedDate = DateTime.UtcNow;
        }
    }
}
