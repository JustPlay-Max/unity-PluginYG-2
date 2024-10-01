using System;
using UnityEngine;
using YG.Insides;
using YG.Utils.Pay;

namespace YG
{
    public class PaymentsCatalogYG : MonoBehaviour
    {
        public bool spawnPurchases = true;
#if UNITY_EDITOR
        [NestedYG(nameof(spawnPurchases)), Tooltip(Langs.t_rootSpawnPurchases)]
#endif
        public Transform rootSpawnPurchases;
#if UNITY_EDITOR
        [NestedYG(nameof(spawnPurchases)), Tooltip(Langs.t_purchasePrefab)]
#endif
        public GameObject purchasePrefab;

        public enum UpdateListMethod { OnEnable, Start, DoNotUpdate };
#if UNITY_EDITOR
        [Tooltip(Langs.t_updateListMethod)]
#endif
        public UpdateListMethod updateListMethod;
#if UNITY_EDITOR
        [Tooltip(Langs.t_purchases)]
#endif
        public PurchaseYG[] purchases = new PurchaseYG[0];

        public Action onUpdatePurchasesList;

        private void OnEnable()
        {
            if (updateListMethod != UpdateListMethod.DoNotUpdate)
                YG2.onGetPayments += UpdatePurchasesList;

            if (updateListMethod == UpdateListMethod.OnEnable)
                UpdatePurchasesList();
        }

        private void OnDisable()
        {
            if (updateListMethod != UpdateListMethod.DoNotUpdate)
                YG2.onGetPayments -= UpdatePurchasesList;
        }

        private void Start()
        {
            if (updateListMethod == UpdateListMethod.Start)
                UpdatePurchasesList();
        }

        public void UpdatePurchasesList()
        {
            if (spawnPurchases)
            {
                DestroyPurchasesList();
                SpawnPurchasesList();
            }
            else
            {
                SetDataPurchasesListByID();
            }
            onUpdatePurchasesList?.Invoke();
        }

        private void DestroyPurchasesList()
        {
            int childCount = rootSpawnPurchases.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                Destroy(rootSpawnPurchases.GetChild(i).gameObject);
            }
        }

        private void SpawnPurchasesList()
        {
            purchases = new PurchaseYG[YG2.purchases.Length];
            for (int i = 0; i < YG2.purchases.Length; i++)
            {
                GameObject purchaseObj = Instantiate(purchasePrefab, rootSpawnPurchases);

                purchases[i] = purchaseObj.GetComponent<PurchaseYG>();
                purchases[i].data = YG2.purchases[i];
                purchases[i].UpdateEntries();
            }
        }

        private void SetDataPurchasesListByID()
        {
            for (int i = 0; i < purchases.Length; i++)
            {
                Purchase purchase = YG2.PurchaseByID(purchases[i].data.id);
                if (purchase != null)
                {
                    purchases[i].data = purchase;
                }
                else
                {
                    Debug.LogError($"Purchase with ID: {purchases[i].data.id} not found!");
                    continue;
                }

                purchases[i].UpdateEntries();
            }
        }

        public void BuyPurchase(string id)
        {
            YG2.BuyPayments(id);
        }
    }
}