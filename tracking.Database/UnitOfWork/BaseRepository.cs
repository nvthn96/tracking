using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using tracking.Model.Entity;

namespace tracking.Database.UnitOfWork
{
	public class BaseRepository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly DbContext _context;

		public BaseRepository(DbContext dbContext)
		{
			_context = dbContext;
		}

		public IEnumerable<T> GetAll()
		{
			var table = _context.Set<T>();
			var output = table.AsNoTracking();
			return output;
		}

		public IEnumerable<T> Filter(Expression<Func<T, bool>> condition)
		{
			var table = _context.Set<T>();
			var output = table.AsNoTracking().Where(condition);
			return output;
		}

		public async Task<T?> FindAsync(Expression<Func<T, bool>> condition)
		{
			var table = _context.Set<T>();
			var output = await table.AsNoTracking().FirstOrDefaultAsync(condition);
			return output;
		}

		public async Task<int> CountAsync()
		{
			var table = _context.Set<T>();
			var output = await table.AsNoTracking().CountAsync();
			return output;
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>> condition)
		{
			var table = _context.Set<T>();
			var output = await table.AsNoTracking().Where(condition).CountAsync();
			return output;
		}

		public async Task<T> AddAsync(T entity)
		{
			var table = _context.Set<T>();

			entity.Id = entity.Id != Guid.Empty ? entity.Id : new Guid();
			entity.CreatedOn = DateTime.UtcNow;

			var output = table.Add(entity);
			await _context.SaveChangesAsync();

			return output.Entity;
		}

		public async Task<T?> UpdateAsync(T entity)
		{
			if (entity.Id == Guid.Empty) return null;

			entity.ModifiedOn = DateTime.UtcNow;

			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return entity;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var table = _context.Set<T>();
			var entity = await table.FindAsync(id);
			if (entity == null) return false;

			entity.IsDeleted = true;
			entity.DeletedOn = DateTime.Now;

			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<int> ClearTableAsync()
		{
			var table = _context.Set<T>();
			var result = await table.ExecuteDeleteAsync();
			return result;
		}
	}
}
