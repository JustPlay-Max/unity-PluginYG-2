namespace YG
{
    public partial interface IPlatformsYG2
    {
        void InitPayments() { }
        void ConsumePurchases() { }
        void ConsumePurchaseByID(string id) { }
        void BuyPayments(string id) { }
    }
}
