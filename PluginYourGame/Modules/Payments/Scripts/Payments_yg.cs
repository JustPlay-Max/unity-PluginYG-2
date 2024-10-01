using System;
using YG.Insides;
using YG.Utils.Pay;

namespace YG
{
    public partial class YG2
    {
        public static Purchase[] purchases = new Purchase[0];
        public static string langPayments = "ru";

        public static Action onGetPayments;
        public static Action<string> onPurchaseSuccess;
        public static Action<string> onPurchaseFailed;

        [InitYG]
        private static void InitPayments()
        {
#if UNITY_EDITOR
            // Reset static for ECS
            onGetPayments = null;
            onPurchaseSuccess = null;
            onPurchaseFailed = null;

            purchases = infoYG.simulationInEditor.purshases;
            langPayments = "ru";
#if EnvirData
            langPayments = infoYG.simulationInEditor.language;
#endif
            onGetPayments?.Invoke();
#else
            iPlatform.InitPayments();
#endif
        }

        public static void BuyPayments(string id)
        {
            GameplayStop(true);
#if !UNITY_EDITOR
            iPlatform.BuyPayments(id);
#else
            Message($"Buy Payment. ID: {id}");
            if (!infoYG.simulationInEditor.testBuyFail)
            {
                YGInsides.OnPurchaseSuccess(id);
            }
            else
            {
                Message($"Buy Payment - Fail Test");
                YGInsides.OnPurchaseFailed(id);
            }
#endif
        }

        public static void ConsumePurchases()
        {
            iPlatform.ConsumePurchases();
        }

        public static void ConsumePurchaseByID(string id)
        {
            iPlatform.ConsumePurchaseByID(id);
        }

        public static Purchase PurchaseByID(string ID)
        {
            for (int i = 0; i < purchases.Length; i++)
            {
                if (purchases[i].id == ID)
                {
                    return purchases[i];
                }
            }

            return null;
        }
    }
}

namespace YG.Utils.Pay
{
    [Serializable]
    public class Purchase
    {
        public string id;
        public string title;
        public string description;
        public string imageURI;
        public string price;
        public string priceValue;
        public string priceCurrencyCode;
        public string currencyImageURL;
        public bool consumed;
    }
}

namespace YG.Insides
{
    public static partial class YGInsides
    {
        public static void OnPurchaseSuccess(string id)
        {
            YG2.onPurchaseSuccess?.Invoke(id);
            YG2.GameplayStart(true);
        }

        public static void OnPurchaseFailed(string id)
        {
            YG2.onPurchaseFailed?.Invoke(id);
            YG2.GameplayStart(true);
        }
    }
}