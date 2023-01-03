using Unity.Entities;

namespace Components.Brain
{
    public struct BrainHealth : IComponentData
    {
        public float CurrentHealth;
        public float MaxHealth;
    }
}