
#if UNITY_EDITOR
using UnityEngine;

namespace YG.Insides
{
    //[CreateAssetMenu(fileName = "AddPlatformSettingsYG", menuName = "YG2/Add Platform Settings")]
    public partial class AddPlatformSettings : ScriptableObject
    {
        public virtual void ApplyProjectSettings() { }
        public virtual void SelectPlatform() { }
        public virtual void DeletePlatform() { }
    }
}
#endif