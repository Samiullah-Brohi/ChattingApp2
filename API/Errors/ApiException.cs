namespace API.Errors;

public class ApiException
{
  public int StatusCode { get; set; }
  public string Message { get; set; } 
  public string Details { get; set; } 
  public ApiException(int statuscode, string message, string details)
  {
    StatusCode = statuscode;
    Message = message;
    Details = details;

  }


}
