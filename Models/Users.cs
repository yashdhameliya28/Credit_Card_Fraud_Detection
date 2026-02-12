using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Users
{
    [Key]
    public long userID { get; set; }

    [Required]
    public string name { get; set; }

    [Required]
    [EmailAddress]
    public string email { get; set; }

    public string? phone { get; set; }

    [Required]
    public string country { get; set; }

    [Column(TypeName = "char(1)")]
    public char gender { get; set; } = 'M';

    public DateTime joinDate { get; set; } = DateTime.UtcNow;

    public string? job { get; set; }

    public string? state { get; set; }

    public long? city_pop { get; set; }

    public ICollection<TxnTable> Transactions { get; set; } = new List<TxnTable>();
    public ICollection<DeviceHistory> deviceHistories { get; set; } = new List<DeviceHistory>();
}