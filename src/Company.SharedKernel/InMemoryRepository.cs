using Ardalis.Specification;

namespace Company.SharedKernel;

public class InMemoryRepository<T> : IRepository<T>
    where T : class, IEntity
{
    readonly Dictionary<int, T> _items = new();
    int _sequence;

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entity, nameof(entity));        
        Guard.Against.OutOfRange(entity.Id, nameof(entity), 0, 0);
        
        entity.SetId(Interlocked.Increment(ref _sequence));
        _items.Add(entity.Id, entity);

        return Task.FromResult(entity);
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entities, nameof(entities));

        foreach (var e in entities)
            await AddAsync(e, cancellationToken);

        return entities;
    }

    public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(specification, nameof(specification));

        return Task.FromResult(specification.Evaluate(_items.Values).Any());
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_items.Any());
    }

    public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(specification, nameof(specification));

        var res = specification.Evaluate(_items.Values).Count();
        return Task.FromResult(res);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_items.Count);
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entity, nameof(entity));
        Guard.Against.NegativeOrZero(entity.Id);

        _items.Remove(entity.Id);
        return Task.CompletedTask;
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entities, nameof(entities));

        foreach (var e in entities)
            await DeleteAsync(e, cancellationToken);        
    }

    public Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_items.Values).FirstOrDefault());
    }

    public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_items.Values).FirstOrDefault());
    }

    public Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return Task.FromResult(_items.GetValueOrDefault((int)(object)id));
    }

    public Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_items.Values).FirstOrDefault());
    }

    public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_items.Values).FirstOrDefault());
    }

    public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_items.Values.ToList());
    }

    public Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_items.Values).ToList());
    }

    public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_items.Values).ToList());
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(0); // TODO: might be an issue
    }

    public Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_items.Values).SingleOrDefault());
    }

    public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_items.Values).FirstOrDefault());
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entity, nameof(entity));
        Guard.Against.NegativeOrZero(entity.Id);

        var target = _items[entity.Id];
        if (target is null)
            throw new InvalidOperationException($"Entity with id {entity.Id} not found.");

        _items[entity.Id] = entity;
        return Task.CompletedTask;
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entities, nameof(entities));

        foreach (var e in entities)
            await UpdateAsync(e, cancellationToken);
    }
}
