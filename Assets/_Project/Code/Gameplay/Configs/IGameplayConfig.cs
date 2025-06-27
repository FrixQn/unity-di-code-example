using DemoProject.Gameplay;
using UnityEngine;

namespace DemoProject.Configs
{
    public interface IGameplayConfig
    {
        public float PlayerSpeed { get; }
        public float PlayerShootForce { get; }
        public Enemy EnemyPrefab { get; }
        public Vector3 EnemyDefaultRotation { get; }
        public Vector3 EnemySpawnOffset { get; }
        public int ScoreToComplete { get; }
        public Vector3 MapSize { get; }
        public Vector3 MapCenter { get; }
    }
}
