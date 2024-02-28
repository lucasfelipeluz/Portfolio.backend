using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
	public Task<User> GetUserByNickName(string nickName);
}
