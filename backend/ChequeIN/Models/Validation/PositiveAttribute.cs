using System.ComponentModel.DataAnnotations;

public class PositiveAttribute : ValidationAttribute {
    public override bool IsValid(object value) {
        float floatval;
        if (float.TryParse(value.ToString(), out floatval)) {
            if (floatval >= 0) {
                return true;
            }
        }
        return false;

    }
}
