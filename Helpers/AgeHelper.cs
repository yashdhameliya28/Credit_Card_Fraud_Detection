namespace Credit_Card_Fraud_Detection.Helpers
{
    public static class AgeHelper
    {
        public static int CalculateAge(DateTime joinDate)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - joinDate.Year;
            if (joinDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

}
