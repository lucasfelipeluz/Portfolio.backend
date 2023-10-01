using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Domain.Entities
{
  [NotMapped]
  public class S3Object
  {
    public string Name { get; set; }
    public MemoryStream InputString { get; set; }
    public string BucketName { get; set; }
    public string Folder { get; set; }
  }
}
