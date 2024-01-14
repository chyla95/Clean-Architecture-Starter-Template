using Sample.Architecture.Application.DataStorage.Repositories;

namespace Sample.Architecture.Application.DataStorage;
public interface IDataUnitOfWork
{
    IUserRepository UserRepository { get; init; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

}
