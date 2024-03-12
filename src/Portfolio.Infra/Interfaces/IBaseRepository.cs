namespace Portfolio.Infra.Interfaces;

public interface IBaseRepository<T>
	where T : class
{
	Task<List<T>> GetAllAsync();
	Task<T> GetByIdAsync(int id);
	Task<T> CreateAsync(T entity);
	T Create(T entity);
	Task<T> UpdateAsync(T entity);
	T Update(T entity);
	Task<T> DeleteAsync(T entity);
	T Delete(T entity);
	Task SaveChangesAsync();
}
