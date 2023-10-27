using System.ComponentModel.DataAnnotations;

namespace NationalParks.Models
{
  public class Park
  {
    public int ParkId { get; set; }
    [Required]
    [StringLength(83, ErrorMessage = "* Please enter a valid entry.")] // Fredericksburg and Spotsylvania County Battlefields Memorial National Military Park, comes in at a whopping 83 characters, including spaces.
    public string Name { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "* Please enter a valid entry.")]
    public string State { get; set; }
    [Required]
    [StringLength(750, ErrorMessage = "* Please enter a valid entry.")]
    public string Description { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "* Please enter a valid entry.")]
    public int AnnualVisitors { get; set; }
  }
}
