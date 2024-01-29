using System.Linq.Expressions;


namespace Application.Persistence.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null!,
                                      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
                                      int? top = null,
                                      int? skip = null,
                                      params string[] includeProperties);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
    }
}
