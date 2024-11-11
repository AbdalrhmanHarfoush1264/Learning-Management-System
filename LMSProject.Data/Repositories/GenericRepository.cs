using LMSProject.Data.Abstracts;
using LMSProject.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LMSProject.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Fileds
        private readonly AppDbContext _context;
        #endregion

        #region Constructors
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Functions

        public async Task<bool> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            var effectedRow = await _context.SaveChangesAsync();
            if (effectedRow <= 0)
                return false;

            return true;
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            var effectedRow = await _context.SaveChangesAsync();
            if (effectedRow <= 0)
                return false;

            return true;
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            var effectedRow = await _context.SaveChangesAsync();
            if (effectedRow <= 0)
                return false;

            return true;
        }
        public async Task<List<T>> GetListAsync()
        {
            var list = await _context.Set<T>().ToListAsync();
            return list;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var element = await _context.Set<T>().FindAsync(id);
            return element;
        }
        public IQueryable<T> GetTableNoTracking()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }
        public IQueryable<T> GetTableAsTracking()
        {
            return _context.Set<T>().AsQueryable();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {


            return await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();

        }
        public async Task RollBack()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        #endregion

    }
}
