using BaseArchiCsharp.Application.Common.Behavior;
using BaseArchiCsharp.Application.Common.Exception;
using BaseArchiCsharp.Application.Interface.InterfaceRepository;
using BaseArchiCsharp.Application.Interface.InterfaceService;
using BaseArchiCsharp.Domain.Entities;

namespace BaseArchiCsharp.Application.Services;

[Service]
public class UserService(IUserRepository userRepository) : IUserFinder, IUserRegister
{
    public async Task<User> RegisterUser(string username, string password, string email)
    {
        if (await userRepository.GetUserByName(username) != null)
            throw new AlreadyCreateException("Username already exists");
        
        User newUser = new User { Email = email, Password = password, Username = username };
        return await userRepository.Create(newUser);
    }

    public async Task<User> ModifyUser(int id, string username, string password, string email)
    {
        if (await userRepository.Get(id) == null)
            throw new NotFoundException("User not found");
        
        User modifyUser = new User{ Email = email, Id = id, Password = password, Username = username };
        return await userRepository.Update(modifyUser);
    }

    public async Task DeleteUser(int id)
    {
        User? userToDelete = await userRepository.Get(id);
        if (userToDelete == null)
            throw new NotFoundException("User not found");
        
        await userRepository.Delete(userToDelete);
    }
    
    public async Task<User> GetUserById(int id)
    {
        return await userRepository.Get(id) ?? throw new NotFoundException("User not found");
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        return await userRepository.GetAll();
    }
}