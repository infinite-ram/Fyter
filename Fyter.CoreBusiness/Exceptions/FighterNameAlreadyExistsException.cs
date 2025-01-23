namespace Fyter.CoreBusiness.Exceptions;

public class FighterNameAlreadyExistsException : Exception
{
    public FighterNameAlreadyExistsException(string message)
        : base(message) { }
}
