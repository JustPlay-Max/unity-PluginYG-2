#if YandexGamePlatform

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public void OnURLDefineDomain(string url)
        {
            url = "https://yandex." + YG2.envir.domain + "/games/" + url;

            if (YG2.envir.domain != null && YG2.envir.domain != "")
            {
                YG2.OnURL(url);
            }
            else
            {
#if RU_YG2
                YG2.Message("OnURL_Yandex_DefineDomain: Домен не определен!");
#else
                YG2.Message("OnURL_Yandex_DefineDomain: Domain not defined!");
#endif
            }
        }
    }
}
#endif