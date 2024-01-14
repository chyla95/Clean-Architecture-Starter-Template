using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Application.DataStorage.Repositories;
public interface IUserRepository : IGenericRepository<User, int>
{

}
