using Microsoft.EntityFrameworkCore;
using tracking.Model.View;

namespace tracking.Database.UnitOfWork
{
	public interface IUnitOfWork<Context> where Context : DbContext, new()
	{
		Task<IEnumerable<V>> RunProcedure<V>(string procedure, (string, object)[] param) where V : BaseView;

		Task<V> RunTransaction<U, V>(U unitOfWork, Func<U, Task<V>> func) where U : IUnitOfWork<Context>;

		Task SaveChangesAsync();
	}
}
