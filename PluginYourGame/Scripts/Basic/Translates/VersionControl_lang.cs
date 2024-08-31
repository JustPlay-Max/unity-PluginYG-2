#if UNITY_EDITOR
namespace YG.Insides
{
    public static partial class Langs
    {
#if RU_YG2
        public const string removeBeforeImport = "Удалять модуль перед обновлением (рекомендуется)";
        public const string projectVersion = "Установленная версия";
        public const string latestVersion = "Доступная версия";
        public const string control = "Контроль";
        public const string updateInfo = "Обновить информацию";
        public const string correctDelete = "Корректное удаление";
        public const string fullDeletePluginYG = "Вы уверены, что хотите полностью удалить PluginYG2 со всеми модулями и другой информацией?";
        public const string fullDeletePluginYGComplete = "Перезагрузите Unity Editor.";
        public const string deleteAll = "Удалить всё";
        public const string deletePackage = "Удалить пакет";
        public const string importDependenciesDialog1 = "Для пакета";
        public const string importDependenciesDialog2 = "требуется сначала импортировать зависимости.\nВ проекте отсутствуют следующие модули:";
        public const string importPackage = "Импорт пакета";
        public const string thirdPartyDialog = "Согласны ли вы загрузить пакет со стороннего ресурса?";
#else
        public const string removeBeforeImport = "Remove the module before updating (recommended)";
        public const string projectVersion = "Installed version";
        public const string latestVersion = "Latest version";
        public const string control = "Control";
        public const string updateInfo = "Update info";
        public const string correctDelete = "Correct deletion";
        public const string fullDeletePluginYG = "Are you sure you want to completely remove PluginYG2 with all modules and other information?";
        public const string fullDeletePluginYGComplete = "Restart the Unity Editor.";
        public const string deleteAll = "Delete all";
        public const string deletePackage = "Delete package";
        public const string importDependenciesDialog1 = "For the package";
        public const string importDependenciesDialog2 = "you need to import dependencies first.\nThe following modules are missing from the project:";
        public const string importPackage = "Import package"; 
        public const string thirdPartyDialog = "Do you agree to download the package from a third-party resource?";
#endif
    }
}
#endif