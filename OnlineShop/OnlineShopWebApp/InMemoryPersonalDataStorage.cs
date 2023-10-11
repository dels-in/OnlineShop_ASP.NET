using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1;

public class InMemoryPersonalDataStorage : IPersonalDataStorage
{
    private readonly IFileStorage _inMemoryFileStorage;

    private readonly List<ValidationModel.PersonalData> _personalDatas;
    public InMemoryPersonalDataStorage(IFileStorage inMemoryFileStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
        _personalDatas = _inMemoryFileStorage.Load<ValidationModel.PersonalData>("PersonalDatas.json");
    }

    public void AddToList(ValidationModel.PersonalData personalData)
    {
        _personalDatas.Add(personalData);
        _inMemoryFileStorage.Save(_personalDatas, "PersonalDatas.json");
    }
}