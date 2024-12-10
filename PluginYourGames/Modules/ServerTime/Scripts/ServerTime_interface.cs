using UnityEngine;

namespace YG
{
    public partial interface IPlatformsYG2
    {
        public long ServerTime() { return (long)Time.realtimeSinceStartup * 1000; }
    }
}