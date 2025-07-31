namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;

public record CreateSaleCustomerDto(
    string ExternalId,
    string Name,
    string Email,
    string Phone,
    string Document
);
