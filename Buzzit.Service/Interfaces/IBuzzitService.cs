using Buzzit.Model.Dtos;

namespace Buzzit.Service.Interfaces
{
    public interface IBuzzitService
    {
        Task<SendSMSResponse> SendSMSAsync(string message, List<string> recipients, int maxSegments = 1, string scheduledTime = null);
        Task<CreditBalanceResponse> GetCreditBalanceAsync();
        Task<bool> TransferCreditsAsync(int fromAccount, int toAccount, int amount);
        Task GetDeliveryReportsAsync(List<int> messageIds);
        Task GetIncomingMessagesAsync();
    }

}