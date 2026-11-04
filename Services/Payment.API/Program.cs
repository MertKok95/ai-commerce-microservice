using Payment.API.Data;
using Payment.API.Models;
using Payment.API.DTOs;
using Microsoft.EntityFrameworkCore;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

var app = builder.Build();

app.MapPost("/api/payments", async (PaymentRequestDto request, PaymentDbContext db) =>
{
    var transaction = request.Adapt<OrderTransaction>();
    
    await Task.Delay(750); 
    bool paymentSuccess = !request.CardToken.Contains("fail");
    
    transaction.Status = paymentSuccess ? "Completed" : "Failed";
    
    db.Transactions.Add(transaction);
    await db.SaveChangesAsync();

    return paymentSuccess 
        ? Results.Ok(new PaymentResultDto(transaction.Id, transaction.Status, "Payment successful."))
        : Results.BadRequest(new PaymentResultDto(transaction.Id, transaction.Status, "Payment declined."));
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    db.Database.EnsureCreated();
}

app.Run();