namespace LRMS.IntegrationTests.Core;

public class EnvironmentVariablesExtractor
{
    private const string CommentSymbol = "#";

    public static Dictionary<string, string> Get(string filePath, List<string> variableNames)
    {
        var envFilePath = FindEnvFile(filePath);
        var result = new Dictionary<string, string>();
        var variablesFromFile = ExtractValuesFromFile(envFilePath, variableNames);
        foreach (var variableName in variableNames)
        {
            var environmentVariable = Environment.GetEnvironmentVariable(variableName);
            if (environmentVariable is not null)
            {
                result[variableName] = environmentVariable;
                continue;
            }

            if (!variablesFromFile.TryGetValue(variableName, out var value))
                throw new Exception($"Variable with name {variableName} not found.");

            result[variableName] = value;
        }
        return result;
    }

    private static Dictionary<string, string> ExtractValuesFromFile(string? filePath, List<string> variableNames)
    {
        if (string.IsNullOrEmpty(filePath))
            return [];

        var dictionary = new Dictionary<string, string>();
        foreach (var line in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith(CommentSymbol))
                continue;

            var parts = line.Split('=', 2);
            if (parts.Length != 2)
                continue;

            var key = parts[0].Trim();
            var value = parts[1].Trim();

            if (!variableNames.Contains(key))
                continue;

            dictionary[key] = value;
        }
        return dictionary;
    }

    private static string? FindEnvFile(string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);
        while (directory != null)
        {
            var envFile = Directory.GetFiles(directory, ".env");
            if (envFile.Length != 0)
                return envFile[0];

            directory = Directory.GetParent(directory)?.FullName;
        }
        return null;
    }
}
