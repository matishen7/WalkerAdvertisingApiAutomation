# Automation Test Suite

## Overview

This project contains an automated test suite for validating the endpoints of a consumer management API. The tests are written in C# using the MSTest framework and utilize RestSharp to handle HTTP requests and responses. The API provides functionality for managing consumer contact information, including creating, reading, updating, and deleting consumers.

## Prerequisites

Before running the tests, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 5.0 or later)
- [Visual Studio](https://visualstudio.microsoft.com/) or any other IDE that supports .NET development
- [NuGet packages](#nuget-packages) required for the tests

### NuGet Packages

The following NuGet packages are used in this project:

- **MSTest.TestFramework**: MSTest test framework used for running the tests.
- **MSTest.TestAdapter**: Adapter for MSTest that integrates with Visual Studio and other test runners.
- **RestSharp**: A simple REST and HTTP API client for .NET, used to send HTTP requests and process responses.

You can install these packages via NuGet Package Manager or by running the following commands in the NuGet Package Manager Console:

```bash
dotnet add package MSTest.TestFramework
dotnet add package MSTest.TestAdapter
dotnet add package RestSharp
```

## Project Structure

The project includes the following key components:

- **`CreateConsumerTests.cs`**: Contains test methods for verifying the Consumer create endpoint.
- **`DeleteConsumerTests.cs`**: Contains test methods for verifying the Consumer Delete endpoint.
- **`UpdateConsumerTests.cs`**: Contains test methods for verifying the Consumer Update endpoint.
- **`GetAllConsumersTests.cs`**: Contains test methods for verifying the Get All Consumer endpoint.
- **`GetConsumerByIdTests.cs`**: Contains test methods for verifying the Get Consumer by id endpoint.
- **`ContactInfo.cs`**: A model class that represents the structure of the consumer data returned by the API.

## Running the Tests

### Using Visual Studio

1. **Open the project**: Load the solution in Visual Studio.
2. **Build the project**: Make sure the project builds successfully before running the tests.
3. **Run the tests**: 
   - Open the Test Explorer in Visual Studio (`Test > Test Explorer`).
   - Click the "Run All" button to execute all tests, or select specific tests to run.

### Using Command Line

You can run the tests from the command line using the following command:

```bash
dotnet test
```

This command will build the project (if necessary) and run all tests in the solution.

## Test Examples

### Get All Consumers

This test retrieves a list of consumers from the API and verifies that the response contains valid consumer data.

```csharp
[TestMethod]
public async Task GetAllConsumers_ShouldReturnSuccessAndConsumers()
{
    var request = new RestRequest("/api/Consumer", Method.Get);
    var response = await _client.ExecuteAsync<List<ContactInfo>>(request);

    Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
    Assert.IsNotNull(response.Data);
    Assert.IsInstanceOfType(response.Data, typeof(List<ContactInfo>));

    var firstConsumer = response.Data[0];
    Assert.IsNotNull(firstConsumer);
}
```

### Create a New Consumer

This test creates a new consumer using the API and verifies that the consumer is created successfully.

```csharp
[TestMethod]
public async Task CreateConsumer_ShouldReturnSuccessAndCreatedConsumer()
{
    var request = new RestRequest("/api/Consumer", Method.Post);
    var newConsumer = new ContactInfo
    {
        FirstName = "John",
        LastName = "Doe",
        Phone = "123-456-7890",
        Email = "john.doe@example.com",
        State = "CA",
        Reason = "New Registration"
    };
    request.AddJsonBody(newConsumer);

    var response = await _client.ExecuteAsync<ContactInfo>(request);

    Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
    Assert.IsNotNull(response.Data);
    Assert.AreEqual(newConsumer.FirstName, response.Data.FirstName);
}
```

## Additional Considerations

- **Test Data**: Ensure that the API is in a known state before running the tests, especially if the tests depend on existing data. You may need to seed the database or mock certain dependencies for consistent results.
- **Error Handling**: The tests currently expect successful responses (HTTP 200 OK). Additional tests could be added to handle error scenarios, such as attempting to retrieve a non-existent consumer or providing invalid input.
- **Environment Configuration**: If the API URL or other configurations change, update the `RestClient` initialization accordingly.
