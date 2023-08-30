namespace Portfolio.Domain.Entities
{
  public class User : Base
  {
    public string Name { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    public DateTime CreatedAt { get; set; }


  }
}
