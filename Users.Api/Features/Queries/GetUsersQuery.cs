using MediatR;
using Users.Api.Models;

namespace Users.Api.Features.Queries;

public class GetUsersQuery : IRequest<QueryResultDto<UserDto>>
{
    public GetUsersQuery(int? page, int? itemsPerPage)
    {
        Page = page ?? 0;
        ItemsPerPage = itemsPerPage ?? 0;
    }

    public int? Page { get; private set; }
    public int? ItemsPerPage { get; private set; }
}
