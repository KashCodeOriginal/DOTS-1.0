using Components.Brain;
using Unity.Entities;

namespace Authoring.Brain
{
    public class BrainBaker : Baker<BrainMono>
    {
        public override void Bake(BrainMono authoring)
        {
            AddComponent<BrainTag>();
            
            AddComponent(new BrainHealth()
            {
                CurrentHealth = authoring.Health,
                MaxHealth = authoring.Health
            });

            AddBuffer<BrainDamageBufferElement>();
        }
    }
}