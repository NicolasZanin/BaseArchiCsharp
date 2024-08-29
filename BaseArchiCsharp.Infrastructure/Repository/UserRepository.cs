using BaseArchiCsharp.Application.Interface.InterfaceRepository;
using BaseArchiCsharp.Domain.Entities;
using BaseArchiCsharp.Infrastructure.Common.Behavior;
using Microsoft.EntityFrameworkCore;

namespace BaseArchiCsharp.Infrastructure.Repository;

[Repository]
public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User> Create(User entity)
    {
        await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<User> Update(User entity)
    {
        context.Users.Update(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task Delete(User entity)
    {
        context.Users.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<User?> Get(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<List<User>> GetAll()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByName(string name)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username == name);
    }
}