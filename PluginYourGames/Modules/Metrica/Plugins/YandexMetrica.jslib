mergeInto(LibraryManager.library, {
    YandexMetricaSend_js: function(eventName) {
        try {
            ym(window.yandexMetricaCounterId, 'reachGoal', UTF8ToString(eventName));
        } catch (e) {
            console.error("Error sending data to Yandex Metrica:", e);
        }
    },

    YandexMetricaSend2_js: function(eventName, eventDataJson) {
        try {
            var data = JSON.parse(UTF8ToString(eventDataJson));
            ym(window.yandexMetricaCounterId, 'reachGoal', UTF8ToString(eventName), data);
        } catch (e) {
            console.error("Error sending data to Yandex Metrica:", e);
        }
    }
});