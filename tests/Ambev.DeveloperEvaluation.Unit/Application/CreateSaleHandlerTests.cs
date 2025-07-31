using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateSaleHandler>>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Handle_ValidRequest_Should_ReturnsSuccessResponse")]
    public async Task Handle_ValidRequest_Should_ReturnsSuccessResponse()
    {
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = CreateSaleHandlerTestData.GenerateValidSale();
        var result = CreateSaleHandlerTestData.GenerateValidResult();

        SetupMapperMocks(command, sale, result);

        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(result.Id);
        createSaleResult.SaleNumber.Should().Be(result.SaleNumber);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Handle_InvalidRequest_Should_ThrowValidationException")]
    public async Task Handle_InvalidRequest_Should_ThrowValidationException()
    {
        var command = new CreateSaleCommand("", new CreateSaleCustomerDto("", "", "", "", ""), 
            new CreateSaleBranchDto("", "", "", "", "", ""), []);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Handle_FiveIdenticalItems_Should_Apply10PercentDiscount")]
    public async Task Handle_FiveIdenticalItems_Should_Apply10PercentDiscount()
    {
        var command = CreateSaleHandlerTestData.GenerateCommandWithQuantity(5);
        var sale = CreateSaleHandlerTestData.GenerateValidSale();
        var result = CreateSaleHandlerTestData.GenerateValidResult();

        SetupMapperMocks(command, sale, result);

        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        createSaleResult.Should().NotBeNull();
        await _saleRepository.Received(1).CreateAsync(
            Arg.Is<Sale>(s => s.Items.Any(i => i.Discount == 0.10m)),
            Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Handle_FifteenIdenticalItems_Should_Apply20PercentDiscount")]
    public async Task Handle_FifteenIdenticalItems_Should_Apply20PercentDiscount()
    {
        var command = CreateSaleHandlerTestData.GenerateCommandWithQuantity(15);
        var sale = CreateSaleHandlerTestData.GenerateValidSale();
        var result = CreateSaleHandlerTestData.GenerateValidResult();

        SetupMapperMocks(command, sale, result);

        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        createSaleResult.Should().NotBeNull();
        await _saleRepository.Received(1).CreateAsync(
            Arg.Is<Sale>(s => s.Items.Any(i => i.Discount == 0.20m)),
            Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Handle_MoreThan20IdenticalItems_Should_ThrowValidationException")]
    public async Task Handle_MoreThan20IdenticalItems_Should_ThrowValidationException()
    {
        var command = CreateSaleHandlerTestData.GenerateCommandWithQuantity(25);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Handle_ThreeIdenticalItems_Should_ApplyNoDiscount")]
    public async Task Handle_ThreeIdenticalItems_Should_ApplyNoDiscount()
    {
        var command = CreateSaleHandlerTestData.GenerateCommandWithQuantity(3);
        var sale = CreateSaleHandlerTestData.GenerateValidSale();
        var result = CreateSaleHandlerTestData.GenerateValidResult();

        SetupMapperMocks(command, sale, result);

        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        createSaleResult.Should().NotBeNull();
        await _saleRepository.Received(1).CreateAsync(
            Arg.Is<Sale>(s => s.Items.All(i => i.Discount == 0m)),
            Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Handle_ValidRequest_When_MapsCommandToSale_Should_ValidateAllProperties")]
    public async Task Handle_ValidRequest_When_MapsCommandToSale_Should_ValidateAllProperties()
    {
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = CreateSaleHandlerTestData.GenerateValidSale();
        var result = CreateSaleHandlerTestData.GenerateValidResult();

        SetupMapperMocks(command, sale, result);

        await _handler.Handle(command, CancellationToken.None);

        _mapper.Received(1).Map<Customer>(Arg.Is<CreateSaleCustomerDto>(c =>
            ValidateCustomerProperties(c, command.Customer)));
        _mapper.Received(1).Map<Branch>(Arg.Is<CreateSaleBranchDto>(b =>
            ValidateBranchProperties(b, command.Branch)));
    }

    [Theory(DisplayName = "Handle_WithDifferentQuantities_When_CreatingSale_Should_ApplyCorrectDiscount")]
    [InlineData(3, 0.0)]
    [InlineData(5, 0.10)]
    [InlineData(15, 0.20)]
    public async Task Handle_WithDifferentQuantities_When_CreatingSale_Should_ApplyCorrectDiscount(int quantity, decimal expectedDiscount)
    {
        var command = CreateSaleHandlerTestData.GenerateCommandWithQuantity(quantity);
        var sale = CreateSaleHandlerTestData.GenerateValidSale();
        var result = CreateSaleHandlerTestData.GenerateValidResult();

        SetupMapperMocks(command, sale, result);

        await _handler.Handle(command, CancellationToken.None);

        await _saleRepository.Received(1).CreateAsync(
            Arg.Is<Sale>(s => ValidateSaleItemDiscount(s, expectedDiscount)),
            Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Handle_ValidCommand_When_CreatingRepository_Should_ValidateRepositoryCallParameters")]
    public async Task Handle_ValidCommand_When_CreatingRepository_Should_ValidateRepositoryCallParameters()
    {
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = CreateSaleHandlerTestData.GenerateValidSale();
        var result = CreateSaleHandlerTestData.GenerateValidResult();

        SetupMapperMocks(command, sale, result);

        await _handler.Handle(command, CancellationToken.None);

        await _saleRepository.Received(1).CreateAsync(
            Arg.Is<Sale>(s => ValidateRepositorySaleProperties(s)),
            Arg.Is<CancellationToken>(ct => ct == CancellationToken.None));
    }

    private void SetupMapperMocks(CreateSaleCommand command, Sale sale, CreateSaleResult result)
    {
        _mapper.Map<Customer>(command.Customer).Returns(sale.Customer);
        _mapper.Map<Branch>(command.Branch).Returns(sale.Branch);
        _mapper.Map<Product>(Arg.Any<CreateSaleProductDto>()).Returns(sale.Items.First().Product);
        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>()).Returns(result);
        _saleRepository.GetBySaleNumberAsync(command.SaleNumber, Arg.Any<CancellationToken>())
            .Returns((Sale?)null);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);
    }

    private static bool ValidateCustomerProperties(CreateSaleCustomerDto actual, CreateSaleCustomerDto expected) =>
        actual.ExternalId == expected.ExternalId &&
        actual.Name == expected.Name &&
        actual.Email == expected.Email &&
        actual.Phone == expected.Phone &&
        actual.Document == expected.Document;

    private static bool ValidateBranchProperties(CreateSaleBranchDto actual, CreateSaleBranchDto expected) =>
        actual.ExternalId == expected.ExternalId &&
        actual.Name == expected.Name &&
        actual.Address == expected.Address &&
        actual.City == expected.City &&
        actual.State == expected.State &&
        actual.PostalCode == expected.PostalCode;

    private static bool ValidateItemsProperties(List<CreateSaleItemDto> actual, List<CreateSaleItemDto> expected) =>
        actual.Count == expected.Count &&
        actual.Zip(expected, (a, e) => ValidateItemProperties(a, e)).All(x => x);

    private static bool ValidateItemProperties(CreateSaleItemDto actual, CreateSaleItemDto expected) =>
        actual.Quantity == expected.Quantity &&
        actual.UnitPrice == expected.UnitPrice &&
        ValidateProductProperties(actual.Product, expected.Product);

    private static bool ValidateProductProperties(CreateSaleProductDto actual, CreateSaleProductDto expected) =>
        actual.ExternalId == expected.ExternalId &&
        actual.Name == expected.Name &&
        actual.Description == expected.Description &&
        actual.Category == expected.Category &&
        actual.Price == expected.Price;

    private static bool ValidateSaleItemDiscount(Sale sale, decimal expectedDiscount) =>
        sale.Items.Any(item => Math.Abs(item.Discount - expectedDiscount) < 0.001m);

    private static bool ValidateRepositorySaleProperties(Sale sale) =>
        !string.IsNullOrEmpty(sale.SaleNumber) &&
        sale.Customer != null &&
        sale.Branch != null &&
        sale.Items.Any() &&
        sale.CreatedAt != default &&
        sale.SaleDate != default;
}
