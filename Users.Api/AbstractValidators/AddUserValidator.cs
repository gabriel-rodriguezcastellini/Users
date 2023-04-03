using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using FluentValidation;
using Users.Api.Features.Commands;

namespace Users.Api.AbstractValidators;

public class AddUserValidator : AbstractValidator<AddUserCommand>
{
    public AddUserValidator(IReadRepository<User> repository)
    {
        RuleFor(x => x.Address).NotEmpty().MaximumLength(256).WithMessage("Please specify an address");
        RuleFor(x => x.Email).NotEmpty().MaximumLength(500).EmailAddress().WithMessage("Please specify an email");
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(256).WithMessage("Please specify a phone");
        RuleFor(x => x.Money).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(256).WithMessage("Please specify a name");
        RuleFor(x => x.Type).IsInEnum();

        RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
        {
            return !await repository.AnyAsync(new UserEmailSpecification(email), cancellation);
        }).WithMessage("Please provide another email");

        RuleFor(x => x.Phone).MustAsync(async (phone, cancellation) =>
        {
            return !await repository.AnyAsync(new UserPhoneSpecification(phone), cancellation);
        }).WithMessage("Please provide another phone");

        RuleFor(x => new { x.Name, x.Address }).MustAsync(async (user, cancellation) =>
        {
            return !await repository.AnyAsync(new UserNameAndAddressSpecification(user.Name, user.Address), cancellation);
        }).WithName("Name and address").WithMessage("Please provide another name and/or address");
    }
}
