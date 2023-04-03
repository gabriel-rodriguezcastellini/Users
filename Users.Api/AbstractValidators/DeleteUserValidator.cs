using FluentValidation;
using Users.Api.Features.Commands;

namespace Users.Api.AbstractValidators;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
