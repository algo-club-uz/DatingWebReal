using System.Net.Http.Json;
using CommonFiles.Models;

namespace DatingFront.Services;

public class RequestService
{
    private readonly HttpClient _httpClient;

    public RequestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /*public async Task Register(CreateUserModel model)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "Accounts/register");
        request.Content = JsonContent.Create(model);
        await _httpClient.SendAsync(request);
        
    }*/
    public async Task Register(CreateUserModel model)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Accounts/register");
            request.Content = JsonContent.Create(model);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            // Handle exception or log the error
            Console.WriteLine($"Error sending HTTP request: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}