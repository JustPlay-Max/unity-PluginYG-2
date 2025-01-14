namespace YG
{
#if UNITY_WEBGL
    using System;
    using System.Runtime.InteropServices;
#endif

    public partial interface IPlatformsYG2
    {
#if PLATFORM_WEBGL
        [DllImport("__Internal")]
        private static extern IntPtr GeneralLanguage_js();
#endif
        string GetLanguage()
        {
#if PLATFORM_WEBGL
            return Marshal.PtrToStringUTF8(GeneralLanguage_js());
#else
            return "en";
#endif
        }
    }
}