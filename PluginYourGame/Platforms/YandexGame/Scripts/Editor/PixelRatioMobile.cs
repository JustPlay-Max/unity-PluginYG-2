#if YandexGamePlatform
namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuild
    {
        public static void SetPixelRatioMobile()
        {
            if (infoYG.basicSettings.pixelRatioEnable)
            {
                string value = infoYG.basicSettings.pixelRatioValue.ToString();
                value = value.Replace(",", ".");

                indexFile = indexFile.Replace("config.devicePixelRatio = 1", "config.devicePixelRatio = " + value);
            }
            else
            {
                indexFile = indexFile.Replace("config.devicePixelRatio = 1;", string.Empty);
            }
        }
    }
}
#endif