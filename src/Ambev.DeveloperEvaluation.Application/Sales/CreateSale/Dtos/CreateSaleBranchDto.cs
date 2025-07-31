namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;

public record CreateSaleBranchDto(
    string ExternalId,
    string Name,
    string Address,
    string City,
    string State,
    string PostalCode
);
