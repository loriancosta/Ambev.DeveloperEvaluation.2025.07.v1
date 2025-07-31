using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale.Results;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CancelSaleHandler> _logger;

    public CancelSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<CancelSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cancelling sale with ID {SaleId}", command.Id);

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");
        }

        if (sale.IsCancelled)
        {
            throw new InvalidOperationException($"Sale with ID {command.Id} is already cancelled");
        }

        sale.Cancel();

        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        _logger.LogInformation("Sale cancelled successfully with ID {SaleId}", updatedSale.Id);

        var saleCancelledEvent = new SaleCancelledEvent
        {
            SaleId = updatedSale.Id,
            SaleNumber = updatedSale.SaleNumber,
            CancelledAt = updatedSale.UpdatedAt ?? DateTime.UtcNow,
            CancellationReason = command.CancellationReason
        };

        _logger.LogInformation("SaleCancelled event: {@SaleCancelledEvent}", saleCancelledEvent);

        return new CancelSaleResult
        {
            Id = updatedSale.Id,
            SaleNumber = updatedSale.SaleNumber,
            IsCancelled = updatedSale.IsCancelled,
            UpdatedAt = updatedSale.UpdatedAt,
            Message = "Sale cancelled successfully"
        };
    }
}
