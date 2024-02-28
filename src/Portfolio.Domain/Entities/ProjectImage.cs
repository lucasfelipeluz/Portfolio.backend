namespace Portfolio.Domain.Entities;

public class ProjectImage : Base
{
	public int ProjectId { get; set; }
	public int ImageId { get; set; }
	public Project Project { get; set; }
	public Image Image { get; set; }
}
