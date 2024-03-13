using Microsoft.EntityFrameworkCore;
using Portfolio.Core.ExceptionHandles;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;

namespace Portfolio.Infra.Interfaces;

public class BaseRepository<T> : IBaseRepository<T>
	where T : Base
{
	private readonly PortfolioContext _context;

	public BaseRepository(PortfolioContext context)
	{
		_context = context;
	}

	public virtual async Task<List<T>> GetAllAsync()
	{
		try
		{
			var entities = await _context.Set<T>().AsNoTracking().ToListAsync();
			return entities;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public virtual async Task<T> GetByIdAsync(int id)
	{
		try
		{
			var entity = await _context.Set<T>().AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
			return entity;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public virtual async Task<T> CreateAsync(T entity)
	{
		try
		{
			_context.Add(entity);
			await _context.SaveChangesAsync();

			return entity;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public virtual T Create(T entity)
	{
		try
		{
			_context.Add(entity);

			return entity;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public virtual async Task<T> UpdateAsync(T entity)
	{
		try
		{
			_context.Update(entity);
			await _context.SaveChangesAsync();

			return entity;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public virtual T Update(T entity)
	{
		try
		{
			_context.Update(entity);

			return entity;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public virtual async Task<T> DeleteAsync(T entity)
	{
		try
		{
			_context.Remove(entity);
			await _context.SaveChangesAsync();

			return entity;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public virtual T Delete(T entity)
	{
		try
		{
			_context.Remove(entity);

			return entity;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public virtual async Task SaveChangesAsync()
	{
		try
		{
			await _context.SaveChangesAsync();
		}
		catch (Exception)
		{
			throw new RepositoryException("Error on save changes");
		}
	}
}
