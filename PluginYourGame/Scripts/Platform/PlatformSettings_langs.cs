#if UNITY_EDITOR
namespace YG.Insides
{
    public static partial class Langs
    {
#if RU_YG2
        public const string projectSettings = "Настройки проекта";
        public const string t_nameDefining = "Имя платформы - используется для Scripting Define Symbols и для класса реализующего интерфейс платформы.";
        public const string namePlatform = "Имя платформы";
        public const string applySettingsProject = "Применить настройки проекта";
        public const string addProjectSettings = "Дополнительные опции";
#else
        public const string projectSettings = "Progect settings";
        public const string t_nameDefining = "Platform name - used for Scripting Define Symbols and for the class implementing the platform interface.";
        public const string namePlatform = "Name platform";
        public const string applySettingsProject = "Apply settings project";
        public const string addProjectSettings = "Additional options";
#endif
    }
}
#endif