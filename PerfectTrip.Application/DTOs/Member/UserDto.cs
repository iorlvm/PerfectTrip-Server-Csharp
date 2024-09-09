using PerfectTrip.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PerfectTrip.Application.DTOs.Member
{
    public class UserDto
    {
        public int UserId { get; set; }

        //public int? CompanyId { get; set; }

        //public int? AdminId { get; set; }

        public string Username { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }

        // Admin specific properties
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdminGroup? AdminGroup { get; set; }

        // Company properties
        public string? CompanyName { get; set; }
        public string? VatNumber { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public float? Score { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PassStatus? Pass { get; set; }

        // Customer properties
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nickname { get; set; }
        public string? TaxId { get; set; }
        public string? Country { get; set; }
        public DateTime? Birthday { get; set; }
        public string? PhoneNumber { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender? Gender { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CustomerGroup? CustomerGroup { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public string? Token { get; set; }

        public string? RefreshToken { get; set; }
    }
}
