namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

public record class DeleteUserRequest
{
    public Guid Id { get; init; }
}