using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Interfaces;
using AutoMapper;
using MediatR;
using Users.Api.AbstractValidators;
using Users.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Api.Features.Commands;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto>
{
    private readonly IRepository<User> repository;
    private readonly IMapper mapper;
    private readonly AddUserValidator rules;
    private readonly IUserService userService;    

    public AddUserCommandHandler(IRepository<User> repository, IMapper mapper, AddUserValidator rules, IUserService userService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.rules = rules;
        this.userService = userService;
    }

    public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        await rules.ValidateAsync(request, cancellationToken);
        var user = new User(request.Name, request.Email, request.Address, request.Phone, request.Type, request.Money);
        userService.AddGift(user);
        user = await repository.AddAsync(user, cancellationToken);
        return mapper.Map<UserDto>(user);
    }
}
