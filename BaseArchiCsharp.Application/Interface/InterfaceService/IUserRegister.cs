using BaseArchiCsharp.Domain.Entities;

namespace BaseArchiCsharp.Application.Interface.InterfaceService;

public interface IUserRegister
{
    Task<User> RegisterUser(string username, string password, string email);
    Task<User> ModifyUser(int id, string username, string password, string email);
    Task DeleteUser(int id);
}