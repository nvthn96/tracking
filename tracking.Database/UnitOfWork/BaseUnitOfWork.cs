using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using tracking.Model.View;

namespace tracking.Database.UnitOfWork
{
	public class BaseUnitOfWork<Context> : IUnitOfWork<Context> where Context : DbContext, new()
	{
		protected readonly Context _context = new();

		public async Task<IEnumerable<V>> RunProcedure<V>(string procedure, (string, object)[] param)
			where V : BaseView
		{
			var query = procedure + " " + string.Join(", ", param.Select(item => item.Item1));
			var sqlParam = param.Select(item => new SqlParameter(item.Item1, item.Item2));

			var output = await _context.Set<V>().FromSqlRaw(query, sqlParam).ToListAsync();

			return output;
		}

		public async Task<V> RunTransaction<U, V>(U unitOfWork, Func<U, Task<V>> func)
			where U : IUnitOfWork<Context>
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var result = await func.Invoke(unitOfWork);
					transaction.Commit();

					return result;
				}
				catch (Exception)
				{
					transaction.Rollback();
				}
			}

			return default!;
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
