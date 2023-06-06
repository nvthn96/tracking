using System.Linq.Expressions;

namespace tracking.Database.UnitOfWork
{
	public interface IRepository<T>
	{
		IEnumerable<T> GetAll();
		IEnumerable<T> Filter(Expression<Func<T, bool>> condition);
		Task<T?> FindAsync(Expression<Func<T, bool>> condition);
		Task<int> CountAsync();
		Task<int> CountAsync(Expression<Func<T, bool>> condition);
		Task<T> AddAsync(T entity);
		Task<T?> UpdateAsync(T entity);
		Task<bool> DeleteAsync(Guid id);
		Task<int> ClearTableAsync();
	}
}
