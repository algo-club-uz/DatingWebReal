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

    public async Task Register(CreateUserModel model)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/Accounts/register");
        request.Content = JsonContent.Create(model);
        await _httpClient.SendAsync(request);
    }
}