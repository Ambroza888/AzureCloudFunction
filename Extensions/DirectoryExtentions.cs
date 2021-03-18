using Microsoft.Extensions.Configuration;

namespace Valence.Extensions
{
    public static class DirectoryExtentions
    {
        public static string GetLocalSettingJson(string FilePath)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            return config[$"ConnectionStrings:{FilePath}"];
        }
    }
}