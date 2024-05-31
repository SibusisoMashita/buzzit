# Buzzen WinSMS Integration Service

Welcome to the Buzzen WinSMS Integration Service! This service allows you to easily send SMS messages, check your credit balance, transfer credits, get delivery reports, and fetch incoming messages using the WinSMS API.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Classes and Interfaces](#classes-and-interfaces)
  - [BuzzitService](#buzzitservice)
  - [IBuzzitService](#ibuzzitservice)
  - [CreditBalanceResponse](#creditbalanceresponse)
  - [Recipient](#recipient)
  - [SendSMSResponse](#sendsmsresponse)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)

## Installation

### Prerequisites

- .NET 5.0 SDK or later
- Visual Studio 2019 or later

### Steps

1. Clone the repository:

```bash
git clone https://github.com/yourusername/Buzzen.git
cd Buzzen
```

2. Open the solution file `Buzzen.sln` in Visual Studio.

3. Restore the dependencies:

```bash
dotnet restore
```

4. Build the project:

```bash
dotnet build
```

## Usage

### Setting Up AWS Credentials

For security reasons, it's recommended to set up your AWS credentials as environment variables. This avoids storing sensitive information directly in your code.

#### Windows

1. Open the Start Menu and search for "Environment Variables."
2. Click on "Edit the system environment variables."
3. In the System Properties window, click on "Environment Variables..."
4. Under "User variables" or "System variables," click "New..."
5. Add a new variable named `AWS_ACCESS_KEY_ID` and paste your access key.
6. Add another variable named `AWS_SECRET_ACCESS_KEY` and paste your secret key.

#### MacOS/Linux

1. Open a terminal.
2. Edit your profile file (e.g., `~/.bashrc`, `~/.bash_profile`, or `~/.zshrc`).
3. Add the following lines:
   ```bash
   export AWS_ACCESS_KEY_ID=your-access-key
   export AWS_SECRET_ACCESS_KEY=your-secret-key
   ```
4. Save the file and source it:
   ```bash
   source ~/.bashrc  # or the appropriate file
   ```

### Example Usage

```csharp
using Buzzit.Service.Implementation;
using System;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "your-winsms-api-key";
        string restClientUrl = "https://rest.winsms.co.za/v1/";

        IBuzzitService buzzitService = new BuzzitService(apiKey, restClientUrl);

        // Send an SMS
        var sendSMSResponse = await buzzitService.SendSMSAsync("Hello World!", new List<string> { "1234567890" });
        Console.WriteLine($"Message sent with status: {sendSMSResponse.StatusCode}");

        // Get credit balance
        var creditBalance = await buzzitService.GetCreditBalanceAsync();
        Console.WriteLine($"Credit Balance: {creditBalance.CreditBalance}");

        // Transfer credits
        bool transferSuccess = await buzzitService.TransferCreditsAsync(1234, 5678, 10);
        Console.WriteLine($"Credit Transfer Status: {transferSuccess}");
    }
}
```

## Classes and Interfaces

### `BuzzitService`

This is the main service class that implements the `IBuzzitService` interface. It contains methods for interacting with the WinSMS API:

- `SendSMSAsync`: Sends an SMS message.
- `GetCreditBalanceAsync`: Retrieves the credit balance.
- `TransferCreditsAsync`: Transfers credits between accounts.
- `GetDeliveryReportsAsync`: Retrieves delivery reports for sent messages.
- `GetIncomingMessagesAsync`: Fetches incoming messages.

### `IBuzzitService`

This interface defines the contract for the `BuzzitService` class. It includes methods for sending SMS, getting credit balance, transferring credits, getting delivery reports, and fetching incoming messages.

### `CreditBalanceResponse`

This class represents the response structure for the credit balance endpoint of the WinSMS API. It contains properties such as `TimeStamp`, `Version`, `StatusCode`, and `CreditBalance`.

### `Recipient`

This class represents the structure of a recipient in the response for the Send SMS endpoint. It includes properties such as `ClientMessageId`, `MobileNumber`, `Accepted`, `AcceptError`, `ApiMessageId`, `ScheduledTime`, `CreditCost`, and `NewCreditBalance`.

### `SendSMSResponse`

This class represents the response structure for the Send SMS endpoint of the WinSMS API. It includes properties such as `TimeStamp`, `Version`, `StatusCode`, and a list of `Recipients`.

## Configuration

Ensure you have your WinSMS API key and the REST client URL configured in your application. These should be set in your environment variables or in a secure configuration file.

## Contributing

We welcome contributions! Please fork the repository and submit pull requests for any enhancements or bug fixes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

This README file provides a comprehensive overview of the Buzzen WinSMS Integration Service, including installation instructions, usage examples, class explanations, configuration details, and contribution guidelines.
