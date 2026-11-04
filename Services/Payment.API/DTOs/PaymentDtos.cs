namespace Payment.API.DTOs;

public record PaymentRequestDto(string ProductId, decimal Amount, string CardToken);
public record PaymentResultDto(int TransactionId, string Status, string Message);