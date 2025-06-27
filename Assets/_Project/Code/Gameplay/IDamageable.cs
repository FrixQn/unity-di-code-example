namespace DemoProject.Gameplay
{
    public delegate void OnDamagedDelegate(IDamageable instance, float damage);

    public interface IDamageable
    {
        public bool IsAlive { get; }
        public event OnDamagedDelegate Damaged;
        void TakeDamage(float damage);
    }
}
