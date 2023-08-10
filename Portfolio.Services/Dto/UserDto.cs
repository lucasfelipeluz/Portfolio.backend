namespace Portfolio.Services.Dto
{
  public class UserDto
  {
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string NickName { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
  }
}