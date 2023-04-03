using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Interfaces;
using AutoMapper;
using MediatR;
using Users.Api.Exceptions;
using Users.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Api.Features.Queries;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IReadRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IReadRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        return user == null ? throw new NotFoundException(nameof(User), request.Id) : _mapper.Map<UserDto>(user);
    }
}
