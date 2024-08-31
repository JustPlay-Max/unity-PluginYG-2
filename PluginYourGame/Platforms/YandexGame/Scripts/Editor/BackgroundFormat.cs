#if YandexGamePlatform
using System.IO;
using UnityEngine;

namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuild
    {
        public static void SetBackgroundFormat()
        {
            string searchCode = @"loadingCover.style.background = ""url('Images/background.png') center / cover"";";

            if (!indexFile.Contains(searchCode))
            {
                Debug.LogWarning("Search string not found in index.html");
                return;
            }

            if (infoYG.basicSettings.backgroundImgFormat == InfoYG.Basic.BackgroundImageFormat.png)
            {
                DeleteImage("jpg");
                DeleteImage("gif");
            }
            else if (infoYG.basicSettings.backgroundImgFormat == InfoYG.Basic.BackgroundImageFormat.jpg)
            {
                indexFile = indexFile.Replace(searchCode, searchCode.Replace("png", "jpg"));
                DeleteImage("png");
                DeleteImage("gif");
            }
            else if (infoYG.basicSettings.backgroundImgFormat == InfoYG.Basic.BackgroundImageFormat.gif)
            {
                indexFile = indexFile.Replace(searchCode, searchCode.Replace("png", "gif"));
                DeleteImage("png");
                DeleteImage("jpg");
            }
            else if (infoYG.basicSettings.backgroundImgFormat == InfoYG.Basic.BackgroundImageFormat.unity)
            {
                if (indexFile.Contains("var backgroundUnity = "))
                    indexFile = indexFile.Replace(searchCode, "canvas.style.background = backgroundUnity;");
                else
                    indexFile = indexFile.Replace(searchCode, string.Empty);

                DeleteImage("png");
                DeleteImage("jpg");
                DeleteImage("gif");
            }
            else if (infoYG.basicSettings.backgroundImgFormat == InfoYG.Basic.BackgroundImageFormat.no)
            {
                indexFile = indexFile.Replace(searchCode, string.Empty);
                DeleteImage("png");
                DeleteImage("jpg");
                DeleteImage("gif");
            }

            void DeleteImage(string format)
            {
                string pathImage = BUILD_PATCH + "/Images/background." + format;

                if (File.Exists(pathImage))
                {
                    File.Delete(pathImage);
                }
            }
        }

    }
}
#endif