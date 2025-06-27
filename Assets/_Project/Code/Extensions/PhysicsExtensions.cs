using UnityEngine;

namespace DemoProject.Extensions
{
    public static class PhysicsExtensions
    {
        public static bool TryGetComponent<T>(this Collision collision, out T component)
        {
            return collision.gameObject.TryGetComponent(out component);
        }
    }
}
