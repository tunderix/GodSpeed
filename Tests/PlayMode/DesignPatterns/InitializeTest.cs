using Ioni.DesignPatterns.Singleton;
using UnityEngine;

namespace Ioni.Tests.PlayMode.DesignPatterns
{
    public class InitializeTest : MonoSingleton<InitializeTest>
    {
        public string S_NAME = "WORKS";
    }
}
