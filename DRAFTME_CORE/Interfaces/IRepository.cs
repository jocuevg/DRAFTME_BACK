namespace DRAFTME_CORE.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query { get; }
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void Attach(TEntity entity);
    public Task SaveChangesAsync();
}
