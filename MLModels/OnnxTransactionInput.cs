namespace Credit_Card_Fraud_Detection.MLModels
{
    public class OnnxTransactionInput
    {
        public string category { get; set; }
        public double amt { get; set; }        
        public string gender { get; set; }
        public string state { get; set; }
        public string job { get; set; }

        public long city_pop { get; set; }     
        public int hour { get; set; }         
        public int day_of_week { get; set; }  
        public int age { get; set; }          
    }

}
