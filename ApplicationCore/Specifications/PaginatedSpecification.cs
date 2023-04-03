using ApplicationCore.Entities.UserAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class PaginatedSpecification : Specification<User>
{
    public PaginatedSpecification(int skip, int take)
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query.AsNoTracking().Skip(skip).Take(take);
    }
}
