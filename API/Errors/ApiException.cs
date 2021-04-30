namespace API.Errors
{
    public class ApiException : ApiErrorResponse
    {
    public ApiException(int status, string message = null, string detail = null) : base(status, message)
    {
        Detail = detail;
    }

    public string Detail { get; set; }
    }
}