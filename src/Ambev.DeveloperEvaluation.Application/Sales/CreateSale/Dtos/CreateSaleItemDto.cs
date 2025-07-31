namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;

public record CreateSaleItemDto(
    CreateSaleProductDto Product,
    int Quantity,
    decimal UnitPrice
);
