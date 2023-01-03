using Components.Zombie;
using Components.Zombie.Eat;
using Components.Zombie.Walk;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using ZombieWalkAspect = Components.Zombie.Walk.ZombieWalkAspect;

namespace Systems.Initialize
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct InitializeZombieSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var zombie in SystemAPI.Query<ZombieWalkAspect>().WithAll<NewZombieTag>())
            {
                ecb.RemoveComponent<NewZombieTag>(zombie.Entity);
                ecb.SetComponentEnabled<ZombieWalkProperties>(zombie.Entity, false);
                ecb.SetComponentEnabled<ZombieEatProperties>(zombie.Entity, false);
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}