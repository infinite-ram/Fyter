namespace Fyter.CoreBusiness.FighterRequestModel.ViewModel;

public class FighterRequestViewModel
{
    public string Name { get; set; } = string.Empty;
    public FighterUpdateStatusEnum UpdateStatusType { get; set; }

    public int FighterRequestId { get; set; }
    public bool FighterRequestHasBeenAdded { get; set; } = false;

    public int OutedatedFighterId { get; set; }
    public bool IsOutdated { get; set; } = true;

    public string UserId { get; set; } = string.Empty;

    public DateTime? DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
}
