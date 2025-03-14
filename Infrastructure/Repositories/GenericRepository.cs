﻿using Application.Interface;
using Domain.Common;
using Domain.DBContext;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ShayanStoreDBContext _dbContext;

    public DbSet<T> Entities { get; }

    public IQueryable<T> Table => Entities;
    public IQueryable<T> TableNoTracking => Entities.AsNoTracking();

    public GenericRepository(ShayanStoreDBContext dbContext)
    {
        _dbContext = dbContext;
        Entities = _dbContext.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        Assert.NotNull(entity);
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        Assert.NotNull(entities);
        await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        Assert.NotNull(entity);
        Entities.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async ValueTask<T> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Entities.FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        Assert.NotNull(entity);
        Entities.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}