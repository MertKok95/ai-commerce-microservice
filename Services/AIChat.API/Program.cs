using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using AIChat.API.DTOs;

var builder = WebApplication.CreateBuilder(args);

var openAiSettings = builder.Configuration.GetSection("OpenAI");
builder.Services.AddKernel();
builder.Services.AddOpenAIChatCompletion(
    modelId: openAiSettings["ModelId"]!,
    apiKey: openAiSettings["ApiKey"]!
);

var app = builder.Build();

app.MapPost("/api/chat", async (ChatRequestDto request, Kernel kernel) =>
{
    var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
    
    var chatHistory = new ChatHistory("Sen yetenekli bir e-ticaret asistanısın. Müşterilere elektronik ve mobilya kategorisindeki ürünlerimizi tanıtıp satışa yönlendirirsin.");
    chatHistory.AddUserMessage(request.Message);
    
    var response = await chatCompletionService.GetChatMessageContentAsync(chatHistory, kernel: kernel);
    
    return Results.Ok(new ChatResponseDto(response.Content ?? "No response generated."));
});

app.Run();