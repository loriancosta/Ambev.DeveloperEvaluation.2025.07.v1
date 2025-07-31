namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

public record class GetUserRequest
{
    public Guid Id { get; init; }
}