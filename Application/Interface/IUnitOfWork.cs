using Domain.Common;

namespace Application.Interface;

public interface IUnitOfWork
{
    IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;
}