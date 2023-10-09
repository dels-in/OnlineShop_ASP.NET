using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1;

public class InMemoryPersonalDataStorage : IPersonalDataStorage
{
    private readonly IFileStorage _inMemoryFileStorage;

    private readonly List<ValidationModel.PersonalData> _personalDatas = new(
        JsonSerializer.Deserialize<List<ValidationModel.PersonalData>>(
            new FileStream("PersonalDatas.json", FileMode.OpenOrCreate)) ?? new());

    public InMemoryPersonalDataStorage(IFileStorage inMemoryFileStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
    }

    public void AddToList(ValidationModel.PersonalData personalData)
    {
        _personalDatas.Add(personalData);
        _inMemoryFileStorage.Save(_personalDatas, "PersonalDatas.json");
    }
}