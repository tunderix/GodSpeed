using Ioni.DP;
using UnityEngine;

namespace Ioni.Utilities
{
    public class UtilityProvider : Singleton<UtilityProvider>
    {
        public MonoBehaviour[] AllMonoBehaviours => FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);
    }
}
