namespace Credit_Card_Fraud_Detection.Dtos
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public char Gender { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Job { get; set; }
        public string? State { get; set; }
        public long? CityPop { get; set; }
    }
}