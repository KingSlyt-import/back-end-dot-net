namespace Back_End_Dot_Net.Models
{
    public class ErrorResponse
    {
        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? Type { get; set; }

        public List<string> Errors { get; set; }

        public ErrorResponse(ErrorTitle title, ErrorStatus status, ErrorType type)
        {
            Title = title.GetDisplayName();
            Status = (int)status;
            Type = type.ToString();
            Errors = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}