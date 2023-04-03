using MediatR;
using Users.Api.Models;

namespace Users.Api.Features.Queries;

public class GetUserQuery : IRequest<UserDto>
{
    public int Id { get; private set; }

    public GetUserQuery(int id)
    {
        Id = id;
    }
}
