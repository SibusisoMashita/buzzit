using Buzzit.Model.Dtos;
using Buzzit.Service.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace Buzzit.Service.Implementation
{
    public class BuzzitService : IBuzzitService
    {
        private readonly string _apiKey;
        private IRestClient _client;

        public BuzzitService(string apiKey, string restClientUrl)
        {
            _apiKey = apiKey;
            _client = new RestClient(restClientUrl);
        }

        public void SetRestClient(IRestClient client)
        {
            _client = client;
        }

        public async Task<SendSMSResponse> SendSMSAsync(string message, List<string> recipients, int maxSegments = 1, string scheduledTime = null)
        {
            var request = new RestRequest("sms/outgoing/send", Method.Post);
            request.AddHeader("AUTHORIZATION", _apiKey);
            request.AddJsonBody(new
            {
                message,
                recipients = recipients.ConvertAll(r => new { mobileNumber = r }),
                maxSegments,
                scheduledTime
            });

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var sendSMSResponse = JsonConvert.DeserializeObject<SendSMSResponse>(response.Content);
                return sendSMSResponse;
            }
            else
            {
                throw new Exception($"Error sending SMS: {response.ErrorMessage}");
            }
        }

        public async Task<CreditBalanceResponse> GetCreditBalanceAsync()
        {
            var request = new RestRequest("credits/balance", Method.Get);
            request.AddHeader("AUTHORIZATION", _apiKey);

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var creditBalanceResponse = JsonConvert.DeserializeObject<CreditBalanceResponse>(response.Content);
                return creditBalanceResponse;
            }
            else
            {
                throw new Exception($"Error retrieving credit balance: {response.ErrorMessage}");
            }
        }

        public async Task<bool> TransferCreditsAsync(int fromAccount, int toAccount, int amount)
        {
            var request = new RestRequest("credits/transfer", Method.Post);
            request.AddHeader("AUTHORIZATION", _apiKey);
            request.AddJsonBody(new
            {
                sendingAccountNumber = fromAccount,
                receivingAccountNumber = toAccount,
                transferQuantity = amount
            });

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error transferring credits: {response.ErrorMessage}");
            }
        }

        public async Task GetDeliveryReportsAsync(List<int> messageIds)
        {
            var request = new RestRequest("sms/outgoing/status", Method.Post);
            request.AddHeader("AUTHORIZATION", _apiKey);
            request.AddJsonBody(messageIds);

            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error getting delivery reports: {response.ErrorMessage}");
            }
        }

        public async Task GetIncomingMessagesAsync()
        {
            var request = new RestRequest("sms/incoming", Method.Get);
            request.AddHeader("AUTHORIZATION", _apiKey);

            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error getting incoming messages: {response.ErrorMessage}");
            }
        }
    }

}