namespace Catalog.API.DTOs;

public record ProductDto(string Id, string Name, string Description, decimal Price, string Category);