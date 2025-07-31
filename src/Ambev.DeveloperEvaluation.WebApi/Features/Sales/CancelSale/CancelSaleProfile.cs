namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

using Application.Sales.CancelSale.Results;
using AutoMapper;

public class CancelSaleProfile : Profile
{
    public CancelSaleProfile()
    {
        CreateMap<CancelSaleResult, CancelSaleResponse>();
    }
}
