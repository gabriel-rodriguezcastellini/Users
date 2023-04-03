using ApplicationCore.Entities.UserAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class UserPhoneSpecification : Specification<User>
{
    public UserPhoneSpecification(string phone)
    {
        Query.AsNoTracking().Where(user => user.Phone == phone);
    }
}
