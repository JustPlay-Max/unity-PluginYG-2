using System;

namespace YG
{
    public partial class YG2
    {
        public static Flag[] flags = new Flag[0];

        [InitYG]
        private static void InitFlags()
        {
#if !UNITY_EDITOR
            iPlatform.InitFlags();
#else
            flags = infoYG.simulationInEditor.flags;
#endif
        }

        public static string GetFlag(string name)
        {
            for (int i = 0; i < flags.Length; i++)
            {
                if (flags[i].name == name)
                    return flags[i].value;
            }
            return null;
        }

        [Serializable]
        public struct Flag
        {
            public string name;
            public string value;
        }
    }
}