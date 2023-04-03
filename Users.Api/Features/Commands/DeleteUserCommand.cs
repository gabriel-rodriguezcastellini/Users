using MediatR;

namespace Users.Api.Features.Commands;

public class DeleteUserCommand : IRequest
{
    public int Id { get; private set; }

    public DeleteUserCommand(int id)
    {
        Id = id;
    }
}
