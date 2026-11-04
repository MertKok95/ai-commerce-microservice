namespace Payment.API.Models;

public class OrderTransaction
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}