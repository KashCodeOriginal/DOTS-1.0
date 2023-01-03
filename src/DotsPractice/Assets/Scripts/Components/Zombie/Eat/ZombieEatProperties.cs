using Unity.Entities;

namespace Components.Zombie.Eat
{
    public struct ZombieEatProperties : IComponentData, IEnableableComponent
    {
        public float DamagePerSecond;
        public float Amplitude;
        public float Frequency;
    }
}