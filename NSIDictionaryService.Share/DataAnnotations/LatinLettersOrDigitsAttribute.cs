using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NSIDictionaryService.Share.DataAnnotations;
public class LatinLettersOrDigitsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string input = value.ToString();
            if (Regex.IsMatch(input, @"^[a-zA-Z0-9\s!@#$%^&*()_+=-]*$"))
            {
                return ValidationResult.Success;
            }
        }
        return new ValidationResult(ErrorMessage ?? "Поле может содержать только латинские буквы, цифры и специальные символы.");
    }
}