using Sample.Architecture.Application.DatabaseAccess.Repositories;
using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Infrastructure.DatabaseAccess.Repositories;
internal class UserRepository(DataContext dataContext) : GenericRepository<User>(dataContext), IUserRepository
{

}
