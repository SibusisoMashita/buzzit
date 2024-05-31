namespace Buzzit.Model.Dtos
{
    public class SendSMSResponse
    {
        public string TimeStamp { get; set; }
        public string Version { get; set; }
        public int StatusCode { get; set; }
        public List<Recipient> Recipients { get; set; }
    }
}