using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Application.Extensions
{
    public static class ApplicationIHostBuilderExtension
    {
        public static IHostBuilder BaseConfigurationn(this IHostBuilder builder, Action<Configuration> setup = null) {
            Configuration config = new Configuration();
            setup?.Invoke(config);
            if (string.IsNullOrEmpty(config.ConfigFolder) || !Directory.Exists(config.ConfigFolder)) {
                return builder;
            }
            builder.ConfigureAppConfiguration((context, configurationBuilder)=> {
                var hostConfigs = configurationBuilder.Sources.ToList();
                configurationBuilder.Sources.Clear();
                var files = _GetFiles(config.ConfigFolder, config.Extension);
                foreach (var file in files) {
                    configurationBuilder.AddJsonFile(file, true, true);
                }
                foreach (var hostConfig in hostConfigs) {
                    configurationBuilder.Add(hostConfig);
                }
            });
            return builder;
        }
        private static string[] _GetFiles( string path, string pattern = null) {
            List<string> files = new List<string>();
            string[] jsonFiles = string.IsNullOrEmpty(pattern) ? Directory.GetFiles(path) : Directory.GetFiles(path, pattern);
            foreach (var jsonFile in jsonFiles) {
                files.Add(jsonFile);
            }
            string[] subDirectories = Directory.GetDirectories(path);
            foreach (var subDirectory in subDirectories) {
                files.AddRange(_GetFiles(subDirectory, pattern));
            }
            return files.ToArray();
        }
    }
}
