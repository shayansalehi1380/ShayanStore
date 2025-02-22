using Domain.Entity;

namespace Application.Interface;

public interface IUnitOfWork
{
    IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;
}