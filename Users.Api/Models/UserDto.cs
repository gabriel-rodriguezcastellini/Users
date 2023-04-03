using ApplicationCore.Entities.UserAggregate;
using System.ComponentModel.DataAnnotations;

namespace Users.Api.Models;

public class UserDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Name { get; set; }

    [Required]
    [MaxLength(500)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    [MaxLength(256)]
    [Phone]
    public string Phone { get; set; }    

    public UserType Type { get; set; }    
    public decimal Money { get; set; }
}
