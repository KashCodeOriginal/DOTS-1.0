using Components.Zombie;
using Components.Zombie.Eat;
using Components.Zombie.Heading;
using Components.Zombie.Rise;
using Components.Zombie.Walk;
using Unity.Entities;

namespace Authoring.Zombie
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
            
            AddComponent(new ZombieEatProperties()
            {
                DamagePerSecond = authoring.ZombieDamagePerSecond,
                Amplitude = authoring.ZombieEatAmplitude,
                Frequency = authoring.ZombieEatFrequency
            });
        }
    }
}