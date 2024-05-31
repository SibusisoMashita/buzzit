namespace Buzzit.Model.Dtos
{
    public class Recipient
    {
        public string ClientMessageId { get; set; }
        public string MobileNumber { get; set; }
        public bool Accepted { get; set; }
        public string AcceptError { get; set; }
        public int ApiMessageId { get; set; }
        public string ScheduledTime { get; set; }
        public int CreditCost { get; set; }
        public int NewCreditBalance { get; set; }
    }
}