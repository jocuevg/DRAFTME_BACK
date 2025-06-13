using DRAFTME_CORE.Interfaces;
using DRAFTME_INFRA.Context;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_INFRA.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DraftMeDBContext context;
    public DbSet<TEntity> Table { get; }

    public Repository(DraftMeDBContext context)
    {
        this.context = context;
        Table = context.Set<TEntity>();
    }
    public IQueryable<TEntity> Query => Table;

    public void Add(TEntity entity) => Table.Add(entity);

    public void Delete(TEntity entity) => Table.Remove(entity);

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();

    public void Update(TEntity entity) => Table.Update(entity);

    public void Attach(TEntity entity) => Table.Attach(entity);
}
