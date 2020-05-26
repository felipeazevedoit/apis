using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebPixPrincipalAPI.Helper
{
    public static class FileSystemManager
    {
        public static string SaveFile(string name, byte[] file)
        {
            try
            {
                var path = $"wwwroot/profile-images/{ name }";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                File.WriteAllBytes(path, file);

                return $"http://localhost:5000/profile-images/{ name }";
            }
            catch(Exception e)
            {
                return string.Empty;
            }
        }

        public static void GetFile(string name, out string path, out string extension, out byte[] file)
        {
            try
            {
                var hdDirectoryInWhichToSearch = new DirectoryInfo("wwwroot/profile-images/");
                var fileInDir = hdDirectoryInWhichToSearch.GetFiles("*" + name + "*.*").FirstOrDefault();

                path = $"http://localhost:5000/profile-images/{ name }";
                extension = fileInDir.Extension;
                file = File.ReadAllBytes($"wwwroot/profile-images/{ name }{ extension }");

            }
            catch(Exception e)
            {
                path = string.Empty;
                extension = string.Empty;
                file = null;
            }
        }
    }
}
