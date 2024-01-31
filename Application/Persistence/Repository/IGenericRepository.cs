using System.Linq.Expressions;


namespace Application.Persistence.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                      int? take = null,
                                      int? skip = null);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task<int> CountAsync();
        Task UpdateAsync(T entity);
    }
}
