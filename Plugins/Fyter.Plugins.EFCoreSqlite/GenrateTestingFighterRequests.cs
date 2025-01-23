using Fyter.CoreBusiness.FighterRequestModel;

namespace Fyter.Plugins.EFCoreSqlite;

public static class GenrateTestingFighterRequests
{
    private static List<FighterRequest> fighterRequests = new();

    public static List<FighterRequest> Execute()
    {
        for (int i = 0; i < 10; i++)
        {
            var FighterRequest = new FighterRequest
            {
                FirstName = $"FirstName{i}",
                LastName = $"LastName{i}",
            };

            fighterRequests.Add(FighterRequest);
        }

        return fighterRequests;
    }
}
