using Sample.Architecture.Application.DataStorage.Repositories;
using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Infrastructure.DataStorage.Repositories;
internal sealed class UserRepository(DataContext dataContext) : GenericRepository<User, int>(dataContext), IUserRepository
{

}