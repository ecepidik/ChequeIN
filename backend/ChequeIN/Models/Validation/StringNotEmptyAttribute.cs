using System;
using System.ComponentModel.DataAnnotations;

public class StringNotEmptyAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (String.IsNullOrEmpty(value.ToString()))
        {
            return false;
        }
        return true;

    }
}
