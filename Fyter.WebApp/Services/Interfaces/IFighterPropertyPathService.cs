using Fyter.CoreBusiness.FighterModel;

namespace Fyter.WebApp.Services.Interfaces;

public interface IFighterPropertyPathService
{
    List<string> GetAllPropertyPaths(Fighter fighter);
}
