#if UNITY_EDITOR
namespace YG.EditorScr
{
    [System.Serializable]
    public class ServerJson
    {
        public string redirection = string.Empty;
        public string documentation = "https://ash-message-bf4.notion.site/PluginYG-d457b23eee604b7aa6076116aab647ed";
        public string chat = "https://t.me/yandexgame_plugin";
        public string video = "https://www.youtube.com/@maxBornysov";
        public string playerImage = "https://justplaygames.ru/public/pluginYG2/images/player.png";
        public string purchaseImage = "https://justplaygames.ru/public/pluginYG2/images/purchase1.png";
        public ModuleJson[] modules = new ModuleJson[0];
    }
}
#endif