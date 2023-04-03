using FluentValidation;
using Users.Api.Features.Queries;

namespace Users.Api.AbstractValidators;

public class GetUserValidator : AbstractValidator<GetUserQuery>
{
    public GetUserValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
