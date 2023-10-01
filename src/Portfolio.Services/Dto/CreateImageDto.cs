namespace Portfolio.Services.Dto
{
  public class CreateImageDto
  {
    public string Name { get; set; }
    public string Folder { get; set; }
    public int? ProjectId { get; set; }
    public int? SkillId { get; set; }
  }
}