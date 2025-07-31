using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Results;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public record CreateSaleCommand(
    string SaleNumber,
    CreateSaleCustomerDto Customer,
    CreateSaleBranchDto Branch,
    List<CreateSaleItemDto> Items
) : IRequest<CreateSaleResult>;
