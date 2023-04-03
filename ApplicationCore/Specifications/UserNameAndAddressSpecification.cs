using ApplicationCore.Entities.UserAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class UserNameAndAddressSpecification : Specification<User>
{
    public UserNameAndAddressSpecification(string name, string address)
    {
        Query.AsNoTracking().Where(user => user.Name == name && user.Address == address);
    }
}
