using System;
using System.IO;
using System.Linq;

namespace Coin.Daemon
{
    public static class Configuration
    {
        public const string AppName = "Coin";
        public const string ConfigurationFileName = "settings.json";
        public static string AppNameNormalized =>
            String.Join("_", AppName.Split(" ")).ToLower();
        public static string BasePath => 
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string DataPath =>
            Path.Combine(BasePath, $".{AppNameNormalized}");
        public static string ConfigurationFilePath =>
            Path.Combine(DataPath, ConfigurationFileName);
    }
}