using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class BasicData : MonoBehaviour
    {
        public Text dataText;

        private void OnEnable()
        {
            YG2.onGetSDKData += DrawText;
            YG2.onFocusWindowGame += OnFocusWindowGame;
        }

        private void OnDisable()
        {
            YG2.onGetSDKData -= DrawText;
            YG2.onFocusWindowGame -= OnFocusWindowGame;
        }

        private void Start()
        {
            DrawText();
        }

        public void DrawText()
        {
            string platformName = YG2.infoYG.basicSettings.platform.nameDefining.ToString();
            string platformStr = "Platform";
            platformName = platformName.Replace(platformStr, string.Empty);
            platformName = $"{platformStr} = <color=#cd5200>{platformName}</color>";

            string focusWindowGame = $"is Focus Window Game = {YG2.isFocusWindowGame.ToString()}";
            string gameplaying = $"is Gameplaying = {YG2.isGameplaying.ToString()}";

            dataText.text = $"{platformName}\n{focusWindowGame}\n{gameplaying}";
        }

        private void OnFocusWindowGame(bool hasFocus)
        {
            DrawText();
        }
    }
}