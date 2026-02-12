namespace Credit_Card_Fraud_Detection.Dtos
{
    public class FraudPredictionResponseDto
    {
        public long TxnId { get; set; }

        public double RiskScore { get; set; }  // 0.0 to 1.0

        public bool IsFraudPredicted { get; set; }  // true if RiskScore > 0.8

        public string Decision { get; set; }  // "approve", "review", "block"

        public string Message { get; set; }

        public DateTime PredictedAt { get; set; } = DateTime.UtcNow;
    }
}