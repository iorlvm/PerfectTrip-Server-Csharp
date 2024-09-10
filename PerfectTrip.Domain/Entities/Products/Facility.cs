using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Domain.Entities.Products
{
    public class Facility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacilityId { get; set; }

        [Required]
        [MaxLength(255)]        
        public string FacilityName { get; set; }

        [Required]
        [MaxLength(255)]
        public string FacilityIcon { get; set; }
    }
}
