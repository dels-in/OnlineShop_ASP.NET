using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Models;

public class ValidationModel : PageModel
{
    public PersonalData Message { get; private set; }

    public void OnPost(PersonalData personalData) => Message = personalData;

    public record PersonalData(string validationFirstName, string validationLastName, string validationEmail,
        string validationAddress, string validationAddress2, string validationCity,
        string validationRegion, string validationPostCode);
}