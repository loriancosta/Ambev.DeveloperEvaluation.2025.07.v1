using Ambev.DeveloperEvaluation.Application.Sales.GetSale.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public record GetSaleCommand(Guid Id) : IRequest<GetSaleResult>;
