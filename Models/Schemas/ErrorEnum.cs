using System.ComponentModel.DataAnnotations;

public enum ErrorTitle
{
    [Display(Name = "One or more validation errors occurred.")]
    ValidationTitle,
    [Display(Name = "User input not found.")]
    ItemsNotFound
}

public enum ErrorType
{
    Validation,
    InvalidInput
}

public enum ErrorStatus 
{
   BadRequest = 400
}