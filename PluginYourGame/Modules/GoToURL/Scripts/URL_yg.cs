using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YG
{
    public partial class YG2
    {
        public static void OnURL(string url)
        {
            Message("URL Transition. url: " + url);
#if !PLATFORM_WEBGL || UNITY_EDITOR
            Application.OpenURL(url);
#else
            OnURLWebGL(url);
#endif
        }

#if PLATFORM_WEBGL
        [DllImport("__Internal")]
        private static extern void OpenURL(string url);
        private static void OnURLWebGL(string url)
        {
            try
            {
                OpenURL(url);
            }
            catch (Exception error)
            {
#if RU_YG2
                Debug.LogError("Первый способ перехода по ссылке не удался! Ошибка:\n" + error + "\nВместо первого метода попробуем вызвать второй метод 'Application.OpenURL'");
#else
                Debug.LogError("The first method of following the link failed! Error:\n" + error + "\nInstead of the first method, let's try to call the second method 'Application.OpenURL'");
#endif
                Application.OpenURL(url);
            }
        }
#endif

        public static void OnURLDefineDomain(string url)
        {
            iPlatform.OnURLDefineDomain(url);
        }
    }
}