using System;
using System.IO;

namespace DespPlus.Data
{
    public class Constants
    {
        public const string DatabaseFilename = "DP.db3";

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
