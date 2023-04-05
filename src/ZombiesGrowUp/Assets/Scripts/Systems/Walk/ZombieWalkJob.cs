using Components.Zombie.Eat;
using Components.Zombie.Walk;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using ZombieWalkAspect = Components.Zombie.Walk.ZombieWalkAspect;

namespace Systems.Walk
{
    public partial struct ZombieWalkJob : IJobEntity
    {
        public ZombieWalkJob(float deltaTime, float brainRadius, EntityCommandBuffer.ParallelWriter commandBuffer) : this()
        {
            _deltaTime = deltaTime;
            _brainRadius = brainRadius;
            _commandBuffer = commandBuffer;
        }

        private readonly float _deltaTime;
        private readonly float _brainRadius;
        private EntityCommandBuffer.ParallelWriter _commandBuffer;

        [BurstCompile]
        private void Execute(ZombieWalkAspect zombieWalkAspect, [EntityIndexInQuery] int sortKey)
        {
            zombieWalkAspect.Walk(_deltaTime);

            if (zombieWalkAspect.IsStoppingDistanceReached(float3.zero, _brainRadius))
            {
                _commandBuffer.SetComponentEnabled<ZombieWalkProperties>(sortKey, zombieWalkAspect.Entity, false);
                _commandBuffer.SetComponentEnabled<ZombieEatProperties>(sortKey, zombieWalkAspect.Entity, true);
            }
        }
    }
}