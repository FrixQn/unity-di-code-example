using UnityEngine;

namespace DemoProject.Gameplay
{
    public delegate void SizeUppedDelegate(int stage);
    public delegate void StageProgressChanged(int score, int stage, float normalizedProgress);
    public delegate void ObjectFalledDelegate(int objectScore);

    public interface IBlackHole
    {
        public Vector3 Position { get; }
        public event SizeUppedDelegate SizeUpped;
        public event StageProgressChanged StageProgressChanged;
        public event ObjectFalledDelegate ObjectFalled;

        void Initialize(float movementSpeed, float shootingForce, Bounds bounds);
        void Shoot();
    }
}
