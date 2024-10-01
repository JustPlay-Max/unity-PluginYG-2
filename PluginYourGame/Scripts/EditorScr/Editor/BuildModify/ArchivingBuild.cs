using System.IO;
using System.IO.Compression;

namespace YG.EditorScr.BuildModify
{
    public static class ArchivingBuild
    {
        public static void Archiving(string pathToBuiltProject)
        {
            InfoYG infoYG = YG2.infoYG;

            if (infoYG.basicSettings.archivingBuild)
            {
                string sign = ".zip";

                int.TryParse(BuildLog.ReadProperty("Build number"), out int buildNumInt);
                buildNumInt += 1;
                string buildName = buildNumInt.ToString();

                if (buildName != null || buildName != "0")
                {
                    sign = "_b" + buildName + ".zip";
                }

                string directory = pathToBuiltProject + sign;

                if (File.Exists(directory))
                {
                    File.Delete(directory);
                }

                ZipFile.CreateFromDirectory(pathToBuiltProject, directory);
            }
        }
    }
}