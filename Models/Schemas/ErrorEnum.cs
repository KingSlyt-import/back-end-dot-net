using System.ComponentModel.DataAnnotations;

public enum ErrorTitle
{
    [Display(Name = "One or more validation errors occurred.")]
    ValidationTitle,
}

public enum ErrorType
{
    Validation
}

public enum ErrorStatus 
{
   BadRequest = 400
}