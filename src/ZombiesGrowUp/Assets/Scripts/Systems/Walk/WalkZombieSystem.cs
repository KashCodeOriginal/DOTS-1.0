using Components.Brain;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems.Walk
{
    [BurstCompile]
    [UpdateAfter(typeof(Rise.RiseZombieSystem))]
    public partial struct WalkZombieSystem : ISystem
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
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var brainEntity = SystemAPI.GetSingletonEntity<BrainTag>();
            var brainScale = SystemAPI.GetComponent<LocalTransform>(brainEntity).Scale;

            var brainRadius = brainScale / 2 + 1f; 
            
            new ZombieWalkJob(deltaTime, brainRadius * brainRadius, 
                ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()).ScheduleParallel();
        }
    }
}