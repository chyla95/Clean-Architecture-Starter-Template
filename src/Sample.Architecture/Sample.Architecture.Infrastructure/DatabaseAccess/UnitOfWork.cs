using Sample.Architecture.Application.DatabaseAccess;
using Sample.Architecture.Application.DatabaseAccess.Repositories;
using Sample.Architecture.Infrastructure.DatabaseAccess.Repositories;

namespace Sample.Architecture.Infrastructure.DatabaseAccess;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;

        UserRepository = new UserRepository(_dataContext);
    }

    public IUserRepository UserRepository {  get; init; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => _dataContext.SaveChangesAsync(cancellationToken);
}
