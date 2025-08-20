using System.ComponentModel.DataAnnotations;

namespace ControllersWithViewSample.Entities;

public class PersonViewModel
{
    [Required]
    [StringLength(20)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(20)]
    public string LastName { get; set; }

    public DateTime GetDate() => DateTime.UtcNow;
}