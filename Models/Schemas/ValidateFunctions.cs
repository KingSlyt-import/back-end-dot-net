namespace Back_End_Dot_Net.Models
{
    public class ErrorResponse
    {
        private ErrorTitle validationTitle;
        private ErrorStatus badRequest;
        private ErrorType validation;

        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? Type { get; set; }

        public List<string> Errors { get; set; }

        public ErrorResponse(ErrorTitle validationTitle, ErrorStatus badRequest, ErrorType validation)
        {
            this.validationTitle = validationTitle;
            this.badRequest = badRequest;
            this.validation = validation;
            Errors = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}