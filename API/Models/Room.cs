using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Room
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = String.Empty;
    public int Capacity { get; set; }
}