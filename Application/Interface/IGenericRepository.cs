﻿using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Interface;

public interface IGenericRepository<T> where T : BaseEntity
{
    DbSet<T> Entities { get; }
    IQueryable<T> Table { get; }
    IQueryable<T> TableNoTracking { get; }
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    ValueTask<T> GetByIdAsync( int id,CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
}