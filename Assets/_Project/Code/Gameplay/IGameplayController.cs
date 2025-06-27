namespace DemoProject.Gameplay
{
    public delegate void ScoreChangedDelegate(int current, int required);
    public delegate void LevelCompletedDelegate();
    public delegate void EnemyDeadDelegate();

    public interface IGameplayController
    {
        public event ScoreChangedDelegate ScoreChanged;
        public event LevelCompletedDelegate LevelCompleted;
        public event EnemyDeadDelegate EnemyDead;

        void RestartGame();
        void Shoot();
        void StartGame();
    }
}
