using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YG.Example
{
    public class ReceivingPurchaseExample : MonoBehaviour
    {
        public Text textExample;

        // Пример Unity событий, на которые можно подписать,
        // например, открытие уведомление о успешности совершения покупки
        public UnityEvent successPurchased;
        public UnityEvent failedPurchased;

        private void OnEnable()
        {
            YG2.onPurchaseSuccess += SuccessPurchased;
            YG2.onPurchaseFailed += FailedPurchased;
        }

        private void OnDisable()
        {
            YG2.onPurchaseSuccess -= SuccessPurchased;
            YG2.onPurchaseFailed -= FailedPurchased;
        }

        private void SuccessPurchased(string id)
        {
            successPurchased?.Invoke();

            textExample.text = "Success purchase - " + id;

            // Ваш код для обработки покупки. Например:
            //if (id == "50")
            //    YandexGame.savesData.money += 50;
            //else if (id == "250")
            //    YandexGame.savesData.money += 250;
            //else if (id == "1500")
            //    YandexGame.savesData.money += 1500;
            //YandexGame.SaveProgress();
        }

        private void FailedPurchased(string id)
        {
            failedPurchased?.Invoke();

            textExample.text = "Failed purchase - " + id;
        }
    }
}