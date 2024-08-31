using UnityEngine;

namespace YG
{
    public partial class YG2Instance : MonoBehaviour
    {
        private void Start()
        {
            YG2.Start();
        }

        public void GetDataInvoke()
        {
            YG2.GetDataInvoke();
        }
    }
}