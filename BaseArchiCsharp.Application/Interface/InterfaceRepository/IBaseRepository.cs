using BaseArchiCsharp.Domain.Common;

namespace BaseArchiCsharp.Application.Interface.InterfaceRepository;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task Delete(T entity);
    Task<T?> Get(int id);
    Task<List<T>> GetAll();
}