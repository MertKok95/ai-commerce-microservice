using AIChat.API.DTOs;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var geminiSettings = builder.Configuration.GetSection("Gemini");

var handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
};
builder.Services.AddSingleton(new HttpClient(handler));

var app = builder.Build();

app.MapPost("/api/chat", async (ChatRequestDto request, HttpClient httpClient) =>
{
    var apiKey = geminiSettings["ApiKey"]?.Trim();
    
    var modelId = geminiSettings["ModelId"]?.Trim() ?? "gemini-3.5-flash-lite";
    
    var url = "https://generativelanguage.googleapis.com/v1beta/interactions";

    var payload = new
    {
        model = modelId,
        input = $"Sen yetenekli bir e-ticaret asistanısın. Müşteriye şu soru için satışa yönlendiren bir yanıt ver: {request.Message}"
    };

    var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
    requestMessage.Headers.Add("x-goog-api-key", apiKey);
    requestMessage.Headers.Add("Api-Revision", "2026-05-20");
    requestMessage.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

    var response = await httpClient.SendAsync(requestMessage);
    var responseContent = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
        return Results.Problem($"API Hatası: {response.StatusCode} - {responseContent}");
    }

    using var jsonResponse = JsonDocument.Parse(responseContent);
    string answer = "Yanıt oluşturulamadı.";
    
    if (jsonResponse.RootElement.TryGetProperty("steps", out var steps))
    {
        foreach (var step in steps.EnumerateArray())
        {
            if (step.GetProperty("type").GetString() == "model_output")
            {
                answer = step.GetProperty("content")[0].GetProperty("text").GetString() ?? answer;
                break;
            }
        }
    }

    return Results.Ok(new ChatResponseDto(answer));
});

app.Run();