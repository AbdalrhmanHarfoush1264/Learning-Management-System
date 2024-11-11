using Microsoft.EntityFrameworkCore.Storage;

namespace LMSProject.Data.Abstracts
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<bool> AddAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(T entity);
        public Task<List<T>> GetListAsync();
        public Task<T> GetByIdAsync(int id);
        public IQueryable<T> GetTableNoTracking();
        public IQueryable<T> GetTableAsTracking();
        public Task<IDbContextTransaction> BeginTransactionAsync();
        public Task CommitAsync();
        public Task RollBack();
    }
}
