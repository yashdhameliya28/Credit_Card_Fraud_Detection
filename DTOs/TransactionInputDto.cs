using System.ComponentModel.DataAnnotations;

namespace Credit_Card_Fraud_Detection.Dtos
{
    public class TransactionInputDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        // From Users table
        [Required]
        public long UserId { get; set; }

        public string Gender { get; set; } = "M";

        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public string? Job { get; set; }

        public string? State { get; set; }

        public long? CityPop { get; set; }  // Can be null, we'll default in controller if needed

        // Optional device info for future rule-based boosts
        public long? DeviceId { get; set; }
    }
}