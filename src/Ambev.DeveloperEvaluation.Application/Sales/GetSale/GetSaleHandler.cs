using Ambev.DeveloperEvaluation.Application.Sales.GetSale.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSaleHandler> _logger;

    public GetSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<GetSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetSaleResult> Handle(GetSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving sale with ID {SaleId}", command.Id);

        var validator = new GetSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ArgumentException("Invalid request: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");
        }

        _logger.LogInformation("Sale retrieved successfully with ID {SaleId}", sale.Id);

        return _mapper.Map<GetSaleResult>(sale);
    }
}
