namespace Buzzit.Model.Dtos
{
    public class CreditBalanceResponse
    {
        public string TimeStamp { get; set; }
        public string Version { get; set; }
        public int StatusCode { get; set; }
        public double CreditBalance { get; set; }
    }
}