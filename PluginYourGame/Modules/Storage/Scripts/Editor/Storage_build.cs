#if YandexGamePlatform
namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuild
    {
        public static void Storage()
        {
            InitFunction("LoadCloud");

            string copyCode = FileTextCopy("CloudStorage_js.js");
            AddIndexCode(copyCode, CodeType.JS);
        }
    }
}
#endif