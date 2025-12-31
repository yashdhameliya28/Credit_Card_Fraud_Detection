namespace Credit_Card_Fraud_Detection.Dtos
{
    public class FraudAlertDto
    {
        public long AlertId { get; set; }
        public long TxnId { get; set; }
        public double RiskScore { get; set; }
        public string Decision { get; set; }
        public string AlertStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}