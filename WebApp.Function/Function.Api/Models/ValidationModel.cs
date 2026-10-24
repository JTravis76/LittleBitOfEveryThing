namespace WebAppFunction.Api.Models;

public class ValidationModel
{
  public string ResultMessage { get; set; }
  public bool IsValid { get; set; }


  public ValidationModel(string resultString, bool valid)
  {
    ResultMessage = resultString;
    IsValid = valid;
  }
}