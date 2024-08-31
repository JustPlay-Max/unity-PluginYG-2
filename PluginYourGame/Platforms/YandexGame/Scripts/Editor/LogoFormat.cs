#if YandexGamePlatform
using System.IO;
using UnityEngine;

namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuild
    {
        public static void SetLogoImageFormat()
        {
            string searchCode = "Images/logo.png";

            if (!indexFile.Contains(searchCode))
            {
                Debug.LogError($"Search string '{searchCode}' not found in index.html");
                return;
            }

            if (infoYG.basicSettings.logoImageFormat == InfoYG.Basic.LogoImgFormat.png)
            {
                DeleteLogo("jpg");
                DeleteLogo("gif");
            }
            else if (infoYG.basicSettings.logoImageFormat == InfoYG.Basic.LogoImgFormat.jpg)
            {
                indexFile = indexFile.Replace(searchCode, searchCode.Replace("png", "jpg"));
                DeleteLogo("png");
                DeleteLogo("gif");
            }
            else if (infoYG.basicSettings.logoImageFormat == InfoYG.Basic.LogoImgFormat.gif)
            {
                indexFile = indexFile.Replace(searchCode, searchCode.Replace("png", "gif"));
                DeleteLogo("png");
                DeleteLogo("jpg");
            }
            else if (infoYG.basicSettings.logoImageFormat == InfoYG.Basic.LogoImgFormat.no)
            {
                indexFile = indexFile.Replace(@"<div id=""unity-logo""><img src=""Images/logo.png""></div>", string.Empty);
                DeleteLogo("png");
                DeleteLogo("jpg");
                DeleteLogo("gif");
            }

            void DeleteLogo(string format)
            {
                string pathImage = BUILD_PATCH + "/Images/logo." + format;

                if (File.Exists(pathImage))
                {
                    File.Delete(pathImage);
                }
            }
        }

    }
}
#endif