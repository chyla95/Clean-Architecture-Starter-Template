using Sample.Architecture.Application.DataStorage;
using Sample.Architecture.Application.DataStorage.Repositories;
using Sample.Architecture.Infrastructure.DataStorage.Repositories;

namespace Sample.Architecture.Infrastructure.DataStorage;
internal sealed class DataUnitOfWork : IDataUnitOfWork
{
    private readonly DataContext _dataContext;

    public DataUnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;

        UserRepository = new UserRepository(_dataContext);
    }

    public IUserRepository UserRepository { get; init; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => _dataContext.SaveChangesAsync(cancellationToken);
}