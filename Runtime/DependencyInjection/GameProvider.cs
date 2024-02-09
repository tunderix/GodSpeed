using Ioni.DP.Singleton;
using UnityEngine;

namespace GS.Runtime.DependencyInjection
{
    public class GameProvider : MSingleton<GameProvider>
    {
        [SerializeField] private string mussu;
    }
}
