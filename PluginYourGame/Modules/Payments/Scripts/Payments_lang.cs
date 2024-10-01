#if UNITY_EDITOR
namespace YG.Insides
{
    public static partial class Langs
    {
#if RU_YG2
        public const string t_rootSpawnPurchases = "Родительский объект для спавна в нём покупок";
        public const string t_purchasePrefab = "Префаб покупки (объект со компонентом PurchaseYG)";
        public const string t_updateListMethod = "Когда следует обновлять список покупок?\nStart - обновлять в методе Start.\nOnEnable - обновлять при каждой активации объекта (в методе OnEnable)\nDoNotUpdate - не обновлять.";
        public const string t_purchases = "Список покупок (PurchaseYG)";
        public const string t_showCurrencyCode = "Добавить код валюты к строке цены. (Например YAN)";
        public const string t_purchaseImageLoad = "Компонент ImageLoadYG для загрузки изображения покупки.";
        public const string t_currencyImageLoad = "Компонент ImageLoadYG для загрузки изображения валюты.";
#else
        public const string t_rootSpawnPurchases = "The parent object for spawning purchases in it";
        public const string t_purchasePrefab = "Purchase prefab (an object with the PurchaseYG component)";
        public const string t_updateListMethod = "When should I update my shopping list?\nStart - update in the Start method.\nOn Enable - update each time the object is activated (in the OnEnable method)\nDoNotUpdate - do not update.";
        public const string t_purchases = "Purchases list (PurchaseYG)";
        public const string t_showCurrencyCode = "Add the currency code to the price line. (For example, YAN)";
        public const string t_purchaseImageLoad = "The ImageLoadYG component for uploading a purchase image.";
        public const string t_currencyImageLoad = "The ImageLoadYG component for uploading a currency image.";
#endif
    }
}
#endif