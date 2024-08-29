using BaseArchiCsharp.Domain.Entities;

namespace BaseArchiCsharp.Application.Interface.InterfaceRepository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByName(string name);
}