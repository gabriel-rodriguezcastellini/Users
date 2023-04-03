using FluentValidation;
using Users.Api.Features.Queries;

namespace Users.Api.AbstractValidators;

public class GetUsersValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(0).When(x => x.Page.HasValue);
        RuleFor(x => x.ItemsPerPage).GreaterThanOrEqualTo(0).When(x => x.ItemsPerPage.HasValue);
    }
}
