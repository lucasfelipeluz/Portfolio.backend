namespace Portfolio.Infra.Cache
{
  public interface ICachingRepository
  {
    T Get<T>(string key);
    void Save<T> (string key, T value);
    void Remove(string key);
  }
}
