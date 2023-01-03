using Components.Brain;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Systems.Eat
{
    [BurstCompile]
    [UpdateAfter(typeof(Walk.WalkZombieSystem))]
    public partial struct EatZombieSystem : ISystem
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

            new EatZombieJob(deltaTime, 
                ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                brainEntity, 
                brainRadius * brainRadius).ScheduleParallel();
        }
    }
}