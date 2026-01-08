using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DeviceHistory
{
    [Key]
    public long deviceID { get; set; }

    [Required]
    public long userID { get; set; }

    public string? deviceName { get; set; }

    [Required]
    public string deviceType { get; set; }  // e.g., 'mobile', 'desktop'

    public DateTime firstUseDate { get; set; } = DateTime.UtcNow;

    public DateTime lastUseDate { get; set; } = DateTime.UtcNow;

    [ForeignKey("userID")]
    public Users? users { get; set; }

    public ICollection<TxnTable> txnTables { get; set; } = new List<TxnTable>();
}

public class DeviceHistoryDTO
{
    [Required]
    public long userID { get; set; }

    public string? deviceName { get; set; }
    [Required]
    public string deviceType { get; set; }
}