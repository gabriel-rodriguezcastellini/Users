using ApplicationCore.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    public Repository(UserContext dbContext) : base(dbContext)
    {
    }
}
