using System;

namespace YG
{
    public static partial class YG2
    {
        public static bool isFocusWindowGame { get { return visibilityWindowGame; } }
        private static bool visibilityWindowGame = true;

        public static Action<bool> onFocusWindowGame;
        public static Action onShowWindowGame, onHideWindowGame;

#if UNITY_EDITOR
        [InitYG]
        private static void InitWindowGame() => UnityEngine.Application.focusChanged += SetFocusWindowGame;
#endif
        public static void SetFocusWindowGame(bool visible)
        {
            if (visible)
            {
                visibilityWindowGame = true;
                GameplayStart(true);

                onFocusWindowGame?.Invoke(true);
                onShowWindowGame?.Invoke();
            }
            else
            {
                GameplayStop(true);
                visibilityWindowGame = false;

                onFocusWindowGame?.Invoke(false);
                onHideWindowGame?.Invoke();
            }
        }
    }
}

namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void SetFocusWindowGame(string visible) => YG2.SetFocusWindowGame(visible == "true");
    }
}