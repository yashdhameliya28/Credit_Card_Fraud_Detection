using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class FraudAlert
{
    [Key]
    public long alertID { get; set; }

    [Required]
    public long txnID { get; set; }

    [Required]
    [Column(TypeName = "float")]
    public double riskScore { get; set; }

    [Required]
    public string decision { get; set; } = "review";

    [Required]
    public string alertStatus { get; set; } = "pending";

    public DateTime createdAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("txnID")]
    public TxnTable? transaction { get; set; }  
}