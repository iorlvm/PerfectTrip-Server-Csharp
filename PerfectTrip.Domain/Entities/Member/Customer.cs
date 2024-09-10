using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using PerfectTrip.Common.Enums;

namespace PerfectTrip.Common.Entities.Member
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(20)]
        public string TaxId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Country { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; } = Gender.Secret;

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CustomerGroup Group { get; set; } = CustomerGroup.Normal;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}