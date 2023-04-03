using ApplicationCore.Entities.UserAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class UserEmailSpecification : Specification<User>
{
    public UserEmailSpecification(string email)
    {
        Query.AsNoTracking().Where(user => user.Email == email);
    }
}
