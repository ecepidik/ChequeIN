using System.Collections;
using System.ComponentModel.DataAnnotations;

public class MinimumLengthAttribute : ValidationAttribute
{
    private readonly int _minElements;

    public MinimumLengthAttribute(int minElements)
    {
        _minElements = minElements;
    }

    public override bool IsValid(object value)
    {
        var list = value as IList;
        if (list != null)
        {
            return list.Count >= _minElements;
        }
        return false;
    }
}
