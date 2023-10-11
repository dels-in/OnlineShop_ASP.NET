using WebApplication1.Models;

namespace WebApplication1;

public interface IPersonalDataStorage
{
    void AddToList(ValidationModel.PersonalData personalData); 
}