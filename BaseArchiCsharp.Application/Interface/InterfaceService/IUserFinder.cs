using BaseArchiCsharp.Domain.Entities;

namespace BaseArchiCsharp.Application.Interface.InterfaceService;

public interface IUserFinder
{
    Task<User> GetUserById(int id);
    Task<List<User>> GetAllUsers();
}