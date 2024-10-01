using UnityEngine;
using UnityEngine.UI;
#if Localization
using YG.Utils.Lang;
#endif
#if TMP_YG2
using TMPro;
#endif

namespace YG
{
    public class GetPlayerYG : MonoBehaviour
    {
        public Text playerNameText;
        public ImageLoadYG photoImageLoad;
#if TMP_YG2
        public TMP_Text playerNameTMP;
#endif

        private void OnEnable()
        {
            YG2.onGetSDKData += DrawPlayerData;
#if Localization
            YG2.onSwitchLang += DrawName;
#endif
            if (YG2.SDKEnabled)
                DrawPlayerData();
        }

        private void OnDisable()
        {
            YG2.onGetSDKData -= DrawPlayerData;
#if Localization
            YG2.onSwitchLang -= DrawName;
#endif
        }

        private void DrawPlayerData()
        {
            DrawPhoto();
#if Localization
            DrawName(YG2.lang);
#else
            DrawName(string.Empty);
#endif
        }

        private void DrawPhoto()
        {
            if (photoImageLoad != null && YG2.player.auth)
                photoImageLoad.Load(YG2.player.photo);
        }

        public void DrawName(string lang)
        {
            if (playerNameText != null)
            {
#if Localization
                if (YG2.player.name == "unauthorized")
                    playerNameText.text = UtilsLang.UnauthorizedTextTranslate(lang);
                else if (YG2.player.name == InfoYG.ANONYMOUS)
                    playerNameText.text = UtilsLang.IsHiddenTextTranslate(lang);
                else playerNameText.text = YG2.player.name;
#else
                playerNameText.text = YG2.player.name;
#endif
            }
#if TMP_YG2
            if (playerNameTMP != null)
            {
#if Localization
                if (YG2.player.name == "unauthorized")
                    playerNameTMP.text = UtilsLang.UnauthorizedTextTranslate(lang);
                else if (YG2.player.name == InfoYG.ANONYMOUS)
                    playerNameTMP.text = UtilsLang.IsHiddenTextTranslate(lang);
                else playerNameTMP.text = YG2.player.name;
#else
                playerNameTMP.text = YG2.player.name;
#endif
            }
#endif
        }
    }
}
