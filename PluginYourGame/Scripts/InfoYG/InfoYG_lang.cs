#if UNITY_EDITOR
namespace YG.Insides
{
    public static partial class Langs
    {
#if RU_YG2
        public const string modulesSettings = "Модули";
        public const string platform = "Платформа";
        public const string applySettingsBySwitchPlatform = "Применять настройки проекта при переключении платформы?";
        public const string logoImageFormat = "Формат логотипа, который отображается при загрузке игры.\n\nAssets/WebGLTemplates/YandexGame/Images/logo.format";
        public const string backgroundImgFormat = "Формат изображения на заднем плане при загрузке игры.\n\nAssets/WebGLTemplates/YandexGame/Images/background.format";
        public const string t_debugInEditor = "Запись лога в консоль в Unity Editor.";
        public const string t_autoGRA = "При запуске игры выполнять метод Game Ready API.\n\nМомент, когда игра загрузила все ресурсы и готова к взаимодействию с пользователем.\n\nЕсли данный параметр 'AutoGRA' включен, то плагин сам выполнит метод Game Ready API сразу после загрузки игры.\n\rЕсли в Вашей игре имеются свои реализации загрузки игры, например, загрузка первой сцены, то Вам необходимо снять галку 'AutoGRA' и самостоятельно выполнять этот метод, когда игра будет полностью загружена. Выполнение метода: `YG2.GameReadyAPI();`";
        public const string t_autoPauseGame = "При просмотре рекламы и при других ситуациях когда это необходимо - игра будет ставиться на паузу.";
        public const string t_archivingBuild = "Включить автоматическую архивацию билда?\n\n •  После успешного создания билда игры, папка с содержанием билда пакуется в zip архив. Подпись _b... это номер билда. Если архив с таким же номером билда уже есть, он перезапишется новым.";
        public const string t_syncInitSDK = "Синхронная загрузка SDK платформы и игры. По умолчанию = false - это означает, что сначала пройдёт полная инициализация SDK платформы, затем игра начнет загружаться. Это даёт наибольшее удобство и гарантию, что при старте все функции SDK доступны для использования. Но загрузка при таком методе может быть чуть дольше. При синхронной загрузке необходимо убедиться в том, что если игра загрузится раньше чем инициализируется SDK, то не произойдёт непредвиденных обстоятельств таких как, например, ошибки из-за использования функций плагина до инициализации SDK или неправильная работа функций плагина при старте игры. Решаются такие проблемы с помощью буферной сцены. Попробуйте использовать опцию Load Scene If SDK Late.";
        public const string t_loadSceneIfSDKLate = "Если игра загрузится раньше, чем инициализируется SDK платформы, то после инициализации произойдёт загрузки выбранной сцены. Попробуйте параметр Load Scene Index оставить 0. Тогда после инициализации текущая сцена перезагрузится. В таком случае проверьте нет ли каких либо проблем из-за того, что загружается рабочая сцена, которая может использовать функции плагина до инициализации SDK. При возникновении проблем, сделайте пустую сцену и поместите её на нулевую позицию. Пустая сцена не вызовет ошибок. В параметр Load Scene Index запишите индекс сцены, которую хотите загрузить при старте игры.";
        public const string t_pixelRatio = "Снижение качества изображения игры в угоду оптимизации для мобильных устройств";
        public const string t_simulationInEditor = "Настройки для симуляции в Unity Editor. Можете тестировать игру, например, на разных языках.";
        public const string advertisement = "Реклама";
        public const string t_advIntervalSimulation = "Симуляция интервала запросов на вызов рекламы в секундах для Unity Editor. Реальный таймер настройте в настройках модуля рекламы.";
        public const string t_advDurationAdv = "Длительность симуляции показа рекламы.";
        public const string t_loadAdv = "Задержка открытия рекламы. Может быть полезна для тестирования уведомления о том, что скоро откроется реклама, перед ёё показом (в момент ожидания рекламы).";

        public const string switchLang = "on English";
        public const string documentation = "Документация";
        public const string helpChat = "Помощь в чате";
        public const string video = "Видео";

        public const string versionsActual = "Все версии актуальные";
        public const string versionsUpdate = "Доступны обновления";
        public const string versionsCritical = "Есть критически важные обновления!";

        public const string importNJSON = "Импортировать Newtonsoft JSON";
        public const string activateNJSONForSave = "Активировать NJSON for Storage";
        public const string deactivateNJSONForSave = "Деактивировать NJSON for Storage";

        public const string resetInfoSettings = "Настройки по умолчанию";
        public const string resetInfoSettings_dialog = "Сбросить все настройки плагина?\n(вернитесь назад с помощью ctrl + z)";

        public const string demoScenesInBuildSettings = "Демо Сцены в Build Settings";
        public const string addDemoScenes = "Добавить демо сцены";
        public const string demoScenes = "Демо сцены:\n";
        public const string demoNotAdded = "Демо сцены не добавлены.";
        public const string deleteAllDemoMaterialsFromProject = "Удалить все демо материалы из проекта";
        public const string removeAllDemoMaterials = "Удалить все демо материалы";
        public const string removeAllDemoDialog = "Все демонстрационные сцены и скрипты будут удалены. Восстановить их можно будет повторным импортом. Демо материалы содержатся в пакете самого плагина и в отдельных модулях.";

#else
        public const string platform = "Platform";
        public const string applySettingsBySwitchPlatform = "Apply project settings when switching platforms?";
        public const string logoImageFormat = "The format of the logo that is displayed when loading the game.\n\nAssets/WebGLTemplates/YandexGame/Images/logo.format";
        public const string backgroundImgFormat = "The format of the image in the background when loading the game.\n\nAssets/WebGLTemplates/YandexGame/Images/background.format";
        public const string t_debugInEditor = "Writing a log to the console in Unity Editor.";
        public const string t_autoGRA = "When starting the game, execute the Game Ready API method.\n\n When the game has loaded all resources and is ready to interact with the user.\n\nIf this 'AutoGRA' parameter is enabled, the plugin will execute the Game Ready API method itself immediately after downloading the game.\n\rIf your game has its own game loading implementations, for example, loading the first scene, then you need to uncheck the 'AutoGRA' checkbox and perform this method yourself when the game is fully loaded. Method execution: `YG2.GameReadyAPI();`";
        public const string t_autoPauseGame = "When viewing ads and in other situations when it is necessary, the game will be paused.";
        public const string t_archivingBuild = "Should I enable automatic build archiving?\n\n • After the successful creation of the game build, the folder with the contents of the build is packed in a zip archive. Signature _b... this is the build number. If there is already an archive with the same build number, it will be overwritten with a new one.";
        public const string t_syncInitSDK = "Synchronous loading of the SDK of the platform and the game. By default = false - this means that the SDK of the platform will be fully initialized first, then the game will start loading. This provides the greatest convenience and guarantees that all SDK functions are available for use at startup. But the download with this method may take a little longer. When loading synchronously, you need to make sure that if the game loads before the SDK is initialized, there will be no unforeseen circumstances such as, for example, errors due to the use of plug-in functions before initializing the SDK or incorrect operation of plug-in functions at the start of the game. Such problems are solved with the help of a buffer scene. Try using the Load Scene If SDK Late option.";
        public const string t_loadSceneIfSDKLate = "If the game loads before the platform SDK is initialized, then the selected scene will load after initialization. Try leaving the Load Scene Index parameter at 0. Then, after initialization, the current scene will reboot. In this case, check if there are any problems due to the fact that the working scene is loading, which can use the functions of the plugin before initializing the SDK. If there are problems, make an empty scene and place it in the zero position. An empty scene will not cause errors. In the Load Scene Index parameter, write down the index of the scene that you want to load at the start of the game.";
        public const string t_pixelRatio = "Reducing the image quality of the game for the sake of optimization for mobile devices";
        public const string t_simulationInEditor = "Simulation settings in Unity Editor. You can test the game, for example, in different languages.";
        public const string advertisement = "Advertisement";
        public const string t_advIntervalSimulation = "Simulation of the interval of ad requests in seconds for Unity Editor. Set up the real timer in the settings of the advertising module.";
        public const string t_advDurationAdv = "The duration of the simulation of the ad display.";
        public const string t_loadAdv = "The delay in opening the ad. It can be useful for testing notifications that an ad is about to open before it is shown (while waiting for an ad).";

        public const string switchLang = "вкл Русский";
        public const string documentation = "Documentation";
        public const string helpChat = "Help in chat";
        public const string video = "Video";
        
        public const string modulesSettings = "Modules settings";
        public const string versionsActual = "All versions are current";
        public const string versionsUpdate = "Updates are available";
        public const string versionsCritical = "There are critical updates!";

        public const string importNJSON = "Import Newtonsoft JSON";
        public const string activateNJSONForSave = "Activate NJSON for Storage";
        public const string deactivateNJSONForSave = "Deactivate NJSON for Storage";

        public const string resetInfoSettings = "Set default settings";
        public const string resetInfoSettings_dialog = "Reset all plugin settings?\n(go back using ctrl + z)";

        public const string demoScenesInBuildSettings = "Demo Scenes in Build Settings";
        public const string addDemoScenes = "Add Demo Scenes";
        public const string demoScenes = "Demo scenes:\n";
        public const string demoNotAdded = "Demo scenes have not been added.";
        public const string deleteAllDemoMaterialsFromProject = "Remove all demo materials from the project";
        public const string removeAllDemoMaterials = "Remove all demo materials";
        public const string removeAllDemoDialog = "All demo scenes and scripts will be deleted. They can be restored by re-importing. Demo materials are contained in the package of the plugin itself and in separate modules.";
#endif
    }
}
#endif