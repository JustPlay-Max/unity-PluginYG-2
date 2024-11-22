#if YandexGamesPlatform_yg
namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuild
    {
        public static void AspectRatio()
        {
            if (infoYG.Templates.fixedAspectRatio)
            {
                string textCopy = ManualFileTextCopy($"{InfoYG.CORE_FOLDER_YG2}/Platforms/YandexGames/Scripts/Editor/AspectRatio/AspectRatio.css");

                string aspectRatio = infoYG.Templates.aspectRatio;
                textCopy = textCopy.Replace("___ASPECT_RATIO___", aspectRatio);

                styleFile += $"\n\n\n{textCopy}";
            }
        }
    }
}
#endif