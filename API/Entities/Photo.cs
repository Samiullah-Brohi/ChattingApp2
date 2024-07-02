using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")]
public class Photo
{
  public int Id { get; set;}
  public String URL { get; set;}
  public bool IsMan { get; set;}
  public String PublicId { get; set;} 
  public int AppUserId { get; set; }
   public AppUser AppUser { get; set; }

}
