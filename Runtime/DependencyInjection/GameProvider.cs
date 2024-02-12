using Ioni.DesignPatterns.Singleton;
using UnityEngine;

namespace GS.Runtime.DependencyInjection
{
    public class GameProvider : MonoSingleton<GameProvider>
    {
        [SerializeField] private string mussu;
    }
}
