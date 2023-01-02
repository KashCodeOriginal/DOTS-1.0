using Components;
using Unity.Entities;

namespace Authoring
{
    public class ZombieBaker : Baker<ZombieMono>
    {
        public override void Bake(ZombieMono authoring)
        {
            AddComponent(new ZombieRiseRate()
            {
                SpawnRateValue = authoring.ZombieRiseInterval
            });
            
            AddComponent(new ZombieWalkProperties()
            {
                WalkSpeed = authoring.ZombieWalkSpeed,
                WalkAmplitude = authoring.ZombieWalkAmplitude,
                WalkFrequency = authoring.ZombieWalkFrequency
            });
            
            AddComponent<ZombieTimer>();
            
            AddComponent<ZombieHeading>();
            
            AddComponent<NewZombieTag>();
        }
    }
}