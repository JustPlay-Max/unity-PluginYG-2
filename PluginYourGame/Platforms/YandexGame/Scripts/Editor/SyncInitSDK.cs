#if YandexGamePlatform
namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuild
    {
        public static void SyncInitSDK()
        {
            if (infoYG.basicSettings.syncInitSDK)
            {
                indexFile = indexFile.Replace("let syncInit = false;", "let syncInit = true;");
            }
        }
    }
}
#endif