namespace Portfolio.Infra.Interfaces;

public interface IBaseRepository<T>
	where T : class
{
	Task<List<T>> GetAllAsync();
	Task<T> GetByIdAsync(int id);
	Task<bool> CreateAsync(T entity);
	Task<T> CreateAsync(T entity, bool returnEntity);
	Task<bool> UpdateAsync(T entity);
	Task<bool> DeleteAsync(int id);
}
