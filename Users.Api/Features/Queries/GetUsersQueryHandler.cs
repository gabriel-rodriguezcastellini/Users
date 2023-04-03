using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using AutoMapper;
using MediatR;
using Users.Api.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Api.Features.Queries;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, QueryResultDto<UserDto>>
{
    private readonly IReadRepository<User> readRepository;
    private readonly IMapper mapper;

    public GetUsersQueryHandler(IReadRepository<User> readRepository, IMapper mapper)
    {
        this.readRepository = readRepository;
        this.mapper = mapper;
    }

    public async Task<QueryResultDto<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return new QueryResultDto<UserDto>
        {
            Items = mapper.Map<List<UserDto>>(await readRepository.ListAsync(new PaginatedSpecification(request.Page.Value * request.ItemsPerPage.Value, request.ItemsPerPage.Value), cancellationToken)),
            TotalItems = await readRepository.CountAsync(cancellationToken)
        };
    }
}
