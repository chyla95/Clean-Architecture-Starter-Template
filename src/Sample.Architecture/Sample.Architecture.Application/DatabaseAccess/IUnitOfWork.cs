using Sample.Architecture.Application.DatabaseAccess.Repositories;

namespace Sample.Architecture.Application.DatabaseAccess;
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; init; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
