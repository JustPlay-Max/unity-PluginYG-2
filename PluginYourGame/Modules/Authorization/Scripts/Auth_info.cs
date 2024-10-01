using System;
using UnityEngine;
using YG.Insides;

namespace YG
{
    public partial class InfoYG
    {
        public AuthorizationSettings Authorization;

        [Serializable]
        public class AuthorizationSettings
        {
#if UNITY_EDITOR
            [Tooltip(Langs.t_scopes)]
#endif
            public bool scopes = true;
            public enum PlayerPhotoSize { small, medium, large };
#if UNITY_EDITOR
            [NestedYG(nameof(scopes)), Tooltip(Langs.t_playerPhotoSize)]
#endif
            public PlayerPhotoSize playerPhotoSize;

            public string GetPlayerPhotoSize()
            {
                if (playerPhotoSize == PlayerPhotoSize.small)
                    return "small";
                else if (playerPhotoSize == PlayerPhotoSize.medium)
                    return "medium";
                else if (playerPhotoSize == PlayerPhotoSize.large)
                    return "large";

                return null;
            }
        }
    }
}