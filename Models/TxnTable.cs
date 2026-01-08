using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TxnTable  
{
    [Key]
    public long txnID { get; set; }

    [Required]
    public long userID { get; set; }

    public long? deviceID { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal amount { get; set; }

    [Required]
    public string category { get; set; }

    public DateTime timeStamp { get; set; } = DateTime.UtcNow;

    public double? modelRiskScore { get; set; }

    public bool? modelIsFraud { get; set; }


    public bool? isFraud { get; set; }

    [ForeignKey("userID")]
    public Users? user { get; set; }

    [ForeignKey("deviceID")]
    public DeviceHistory? device { get; set; }

    public ICollection<FraudAlert> FraudAlerts { get; set; } = new List<FraudAlert>();
}