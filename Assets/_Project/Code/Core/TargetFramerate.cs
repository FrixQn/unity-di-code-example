using UnityEngine;

namespace DemoProject.Core
{
    internal static class TargetFramerate
    {
        [RuntimeInitializeOnLoadMethod]
        private static void SetupFramerate()
        {
            Application.targetFrameRate = 120;
        }
    }
}
