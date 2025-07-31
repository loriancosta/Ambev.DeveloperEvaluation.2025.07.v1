using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Results;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating sale with number {SaleNumber}", command.SaleNumber);

        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }

        var existingSale = await _saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
        if (existingSale != null)
        {
            throw new InvalidOperationException($"Sale with number {command.SaleNumber} already exists");
        }

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = command.SaleNumber,
            Customer = _mapper.Map<Customer>(command.Customer),
            Branch = _mapper.Map<Branch>(command.Branch),
            Items = []
        };

        foreach (var itemDto in command.Items)
        {
            var product = _mapper.Map<Product>(itemDto.Product);
            var validationResult2 = sale.AddItem(product, itemDto.Quantity, itemDto.UnitPrice);
            
            if (!validationResult2.IsValid)
            {
                throw new ArgumentException("Invalid item data: " + string.Join(", ", validationResult2.Errors.Select(e => e.Detail)));
            }
        }


        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        _logger.LogInformation("Sale created successfully with ID {SaleId}", createdSale.Id);

        var saleCreatedEvent = new SaleCreatedEvent
        {
            SaleId = createdSale.Id,
            SaleNumber = createdSale.SaleNumber,
            Customer = createdSale.Customer,
            Branch = createdSale.Branch,
            TotalAmount = createdSale.TotalAmount,
            CreatedAt = createdSale.CreatedAt,
            Items = createdSale.Items
        };

        _logger.LogInformation("SaleCreated event: {@SaleCreatedEvent}", saleCreatedEvent);

        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}
