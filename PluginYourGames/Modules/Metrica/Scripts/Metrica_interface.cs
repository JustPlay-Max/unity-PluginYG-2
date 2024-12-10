namespace YG
{
    public partial interface IPlatformsYG2
    {
        void MetricaSend(string eventName, string eventData) { }
        void MetricaSend(string eventName) { }
    }
}
