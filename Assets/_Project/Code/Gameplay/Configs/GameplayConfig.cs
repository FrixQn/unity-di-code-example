using DemoProject.Gameplay;
using UnityEngine;

namespace DemoProject.Configs
{
    [CreateAssetMenu(fileName = nameof(GameplayConfig), menuName = "Project/Configs/GameplayConfig")]
    public class GameplayConfig : ScriptableObject, IGameplayConfig
    {
        [Header("Player")]
        [field: SerializeField] public float PlayerSpeed { get; private set; } = 2f;
        [field: SerializeField, Min(1f)] public float PlayerShootForce { get; private set; } = 100f;
        [Header("Enemy")]
        [field: SerializeField] public Enemy EnemyPrefab { get; private set; }
        [field: SerializeField] public Vector3 EnemyDefaultRotation { get; private set; } = Vector3.up * 180f;
        [field: SerializeField] public Vector3 EnemySpawnOffset { get; private set; } = Vector3.forward * 10f;
        [field: SerializeField] public int ScoreToComplete { get; private set; } = 25;
        [Header("Map")]
        [field: SerializeField] public Vector3 MapCenter { get; private set; } = Vector3.zero;
        [field: SerializeField] public Vector3 MapSize { get; private set; } = Vector3.one;
    }
}
