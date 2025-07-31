using Ambev.DeveloperEvaluation.Application.Sales.CancelSale.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public record CancelSaleCommand(
    Guid Id,
    string CancellationReason = ""
) : IRequest<CancelSaleResult>;
