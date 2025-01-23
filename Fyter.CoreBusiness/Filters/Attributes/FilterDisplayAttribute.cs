namespace Fyter.CoreBusiness.Filters.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class FilterDisplayAttribute : Attribute
{
    public string DisplayName { get; }
    public string OperatorString { get; }

    public FilterDisplayAttribute(string displayName, string operatorString)
    {
        DisplayName = displayName;
        OperatorString = operatorString;
    }
}
