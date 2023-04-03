using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Interfaces;
using MediatR;
using Users.Api.AbstractValidators;
using Users.Api.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Api.Features.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IRepository<User> _userRepository;
    private readonly DeleteUserValidator _validator;

    public DeleteUserCommandHandler(IRepository<User> userRepository, DeleteUserValidator validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAsync(request, cancellationToken);
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(User), request.Id);
        await _userRepository.DeleteAsync(user, cancellationToken);
    }
}
