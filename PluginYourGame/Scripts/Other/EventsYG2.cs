using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace YG
{
    public class EventsYG2 : MonoBehaviour
    {
        public enum EventYG2Type
        {
            GetSDKData,
            PauseStopGame,
            PauseResumeGame,

            OpenInterAdv,
            CloseInterAdv,
            ErrorInterAdv,

            OpenRewardedAdv,
            CloseRewaededAdv,
            RewardAdv,
            ErrorRewardedAdv,

            AuthTrue,
            AuthFalse,

            PurchaseSuccess,
            PurchaseFailed,

            ReviewSuccess,
            ReviewFailed,

            GameLabelSuccess,
            GameLabelFail
        }

        [Serializable]
        public class TriggerEvent : UnityEvent { }

        [Serializable]
        public class Entry
        {
            public EventYG2Type eventID = EventYG2Type.GetSDKData;
            public TriggerEvent callback = new TriggerEvent();
        }

        [FormerlySerializedAs("delegates")]
        [SerializeField]
        private List<Entry> m_Delegates;

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("Please use triggers instead (UnityUpgradable) -> triggers", true)]
        public List<Entry> delegates { get { return triggers; } set { triggers = value; } }

        public List<Entry> triggers
        {
            get
            {
                if (m_Delegates == null)
                    m_Delegates = new List<Entry>();
                return m_Delegates;
            }
            set { m_Delegates = value; }
        }

        private void Execute(EventYG2Type id)
        {
            var triggerCount = triggers.Count;

            for (int i = 0, imax = triggers.Count; i < imax; ++i)
            {
                var ent = triggers[i];
                if (ent.eventID == id && ent.callback != null)
                    ent.callback?.Invoke();
            }
        }

        private void OnEnable()
        {
            YG2.onGetSDKData += OnGetSDKData;
            YG2.onPauseGame += OnPauseGame;
#if InterstitialAdv
            YG2.onOpenInterAdv += OnOpenInterAdv;
            YG2.onCloseInterAdv += OnCloseInterAdv;
            YG2.onErrorInterAdv += OnErrorInterAdv;
#endif
#if RewardedAdv
            YG2.onOpenRewardedAdv += OnOpenRewardedAdv;
            YG2.onCloseRewaededAdv += OnCloseRewaededAdv;
            YG2.onRewardAdv += OnRewardAdv;
            YG2.onErrorRewardedAdv += OnErrorRewardedAdv;
#endif
#if Authorization
            YG2.onAuthTrue += OnAuthTrue;
            YG2.onAuthFalse += OnAuthFalse;
#endif
#if Payments
            YG2.onPurchaseSuccess += OnPurchaseSuccess;
            YG2.onPurchaseFailed += OnPurchaseFailed;
#endif
#if Review
            YG2.onReviewSent += OnReviewSent;
#endif
#if GameLabel
            YG2.onGameLabelSuccess += OnGameLabelSuccess;
            YG2.onGameLabelFail += OnGameLabelFail;
#endif
        }

        private void OnDisable()
        {
            YG2.onGetSDKData -= OnGetSDKData;
            YG2.onPauseGame -= OnPauseGame;
#if InterstitialAdv
            YG2.onOpenInterAdv -= OnOpenInterAdv;
            YG2.onCloseInterAdv -= OnCloseInterAdv;
            YG2.onErrorInterAdv -= OnErrorInterAdv;
#endif
#if RewardedAdv
            YG2.onOpenRewardedAdv -= OnOpenRewardedAdv;
            YG2.onCloseRewaededAdv -= OnCloseRewaededAdv;
            YG2.onRewardAdv -= OnRewardAdv;
            YG2.onErrorRewardedAdv -= OnErrorRewardedAdv;
#endif
#if Authorization
            YG2.onAuthTrue -= OnAuthTrue;
            YG2.onAuthFalse -= OnAuthFalse;
#endif
#if Payments
            YG2.onPurchaseSuccess -= OnPurchaseSuccess;
            YG2.onPurchaseFailed -= OnPurchaseFailed;
#endif
#if Review
            YG2.onReviewSent -= OnReviewSent;
#endif
#if GameLabel
            YG2.onGameLabelSuccess -= OnGameLabelSuccess;
            YG2.onGameLabelFail -= OnGameLabelFail;
#endif
        }

        public void _GameReadyAPI() => YG2.GameReadyAPI();
        public void _GameplayStart() => YG2.GameplayStart();
        public void _GameplayStop() => YG2.GameplayStop();

        private void OnGetSDKData() => Execute(EventYG2Type.GetSDKData);
        private void OnPauseGame(bool pause)
        {
            if (pause) Execute(EventYG2Type.PauseStopGame);
            else Execute(EventYG2Type.PauseResumeGame);
        }

#if InterstitialAdv
        public void _InterstitialAdvShow() => YG2.InterstitialAdvShow();
        public void _ResetTimerInterAdv() => YG2.ResetTimerInterAdv();

        private void OnOpenInterAdv() => Execute(EventYG2Type.OpenInterAdv);
        private void OnCloseInterAdv() => Execute(EventYG2Type.CloseInterAdv);
        private void OnErrorInterAdv() => Execute(EventYG2Type.ErrorInterAdv);
#endif
#if RewardedAdv
        public void _RewardedAdvShow(string id) => YG2.RewardedAdvShow(id);

        private void OnOpenRewardedAdv() => Execute(EventYG2Type.OpenRewardedAdv);
        private void OnCloseRewaededAdv() => Execute(EventYG2Type.CloseRewaededAdv);
        private void OnRewardAdv(string id) => Execute(EventYG2Type.RewardAdv);
        private void OnErrorRewardedAdv() => Execute(EventYG2Type.ErrorRewardedAdv);
#endif
#if StickyAdv
        public void _StickyAdActivity(bool activity) => YG2.StickyAdActivity(activity);
#endif
#if Authorization
        public void _GetAuth() => YG2.GetAuth();
        public void _OpenAuthDialog() => YG2.OpenAuthDialog();

        private void OnAuthTrue() => Execute(EventYG2Type.AuthTrue);
        private void OnAuthFalse() => Execute(EventYG2Type.AuthFalse);
#endif
#if EnvirData
        public void _GetEnvirData() => YG2.GetEnvirData();
#endif
#if Localization
        public void _GetLanguage() => YG2.GetLanguage();
        public void _SwitchLanguage(string language) => YG2.SwitchLanguage(language);
#endif
#if Payments
        public void _GetPayments() => YG2.GetPayments();
        public void _BuyPayments(string id) => YG2.BuyPayments(id);
        public void _ConsumePurchases() => YG2.ConsumePurchases();

        private void OnPurchaseSuccess(string id) => Execute(EventYG2Type.PurchaseSuccess);
        private void OnPurchaseFailed(string id) => Execute(EventYG2Type.PurchaseFailed);
#endif
#if Storage
        public void _SetDefaultSaves() => YG2.SetDefaultSaves();
        public void _SaveProgress() => YG2.SaveProgress();
        public void _LoadProgress() => YG2.LoadProgress();
#endif
#if Fullscreen
        public void _SetFullscreen(bool fullscreen) => YG2.SetFullscreen(fullscreen);
#endif
#if GoToURL
        public void _OnURLDefineDomain(string url) => YG2.OnURLDefineDomain(url);
        public void _OnURL(string url) => YG2.OnURL(url);
#endif
#if Review
        public void _ReviewShow() => YG2.ReviewShow();

        private void OnReviewSent(bool complete)
        {
            if (complete)
                Execute(EventYG2Type.ReviewSuccess);
            else
                Execute(EventYG2Type.ReviewFailed);
        }
#endif
#if GameLabel
        public void _GameLabelShowDialog() => YG2.GameLabelShowDialog();

        private void OnGameLabelSuccess() => Execute(EventYG2Type.GameLabelSuccess);
        private void OnGameLabelFail() => Execute(EventYG2Type.GameLabelFail);
#endif
    }
}
