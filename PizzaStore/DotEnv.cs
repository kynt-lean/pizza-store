namespace Microsoft.Extensions.Configuration;

public static class DotEnv
{
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            int indexOfEquals = line.IndexOf('=');

            if (indexOfEquals == -1)
                continue;

            string keyPart = line[..indexOfEquals];
            string valuePart = indexOfEquals < line.Length ? line[(indexOfEquals + 1)..] : string.Empty;
            Environment.SetEnvironmentVariable(keyPart.Trim(), valuePart.Trim());
        }
    }
}