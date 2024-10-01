using UnityEngine;

namespace YG
{
    public class ConsumePurchasesYG : MonoBehaviour
    {
        private void OnEnable() => YG2.onGetSDKData += ConsumePurchases;
        private void OnDisable() => YG2.onGetSDKData -= ConsumePurchases;

        private static bool consume;

        private void Start()
        {
            if (YG2.SDKEnabled)
                ConsumePurchases();
        }

        private void ConsumePurchases()
        {
            if (!consume)
            {
                consume = true;
                YG2.ConsumePurchases();
            }
        }
    }
}