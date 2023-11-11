namespace WebApplication1;

public static class TokenStorage
{
    public static string GetClientInfo(string path, bool isId)
    {
        var textReader = new StreamReader(path);
        var token = textReader.ReadToEnd().Split("\n");
        textReader.Close();

        return isId ? token[0] : token[1];
    }
}