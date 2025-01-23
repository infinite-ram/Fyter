using System.ComponentModel.DataAnnotations;

namespace Fyter.CoreBusiness.FighterRequestModel;

public class FighterRequest
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public bool HasBeenAdded { get; set; } = false;

    public string AddByUserId { get; set; } = string.Empty;

    public DateTime? DateCreated { get; set; } = null;
    public DateTime? DateEdited { get; set; } = null;
}
