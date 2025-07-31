namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;

public record CreateSaleProductDto(
    string ExternalId,
    string Name,
    string Description,
    string Category,
    decimal Price
);
