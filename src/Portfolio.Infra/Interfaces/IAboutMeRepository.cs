using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Interfaces;

public interface IAboutMeRepository : IBaseRepository<AboutMe>
{
	Task<AboutMe> GetAboutMeAsync();
	Task<bool> UpdateAboutMeAsync(AboutMe aboutMe);
}
