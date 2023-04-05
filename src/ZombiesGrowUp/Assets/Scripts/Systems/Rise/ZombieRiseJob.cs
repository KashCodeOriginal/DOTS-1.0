using Components.Zombie.Rise;
using Components.Zombie.Walk;
using Unity.Burst;
using Unity.Entities;
using ZombieRiseAspect = Components.Zombie.Rise.ZombieRiseAspect;

namespace Systems.Rise
{
    [BurstCompile]
    public partial struct ZombieRiseJob : IJobEntity
    {
        public ZombieRiseJob(float deltaTime, EntityCommandBuffer.ParallelWriter createCommandBuffer) : this()
        {
            _deltaTime = deltaTime;
            _commandBuffer = createCommandBuffer;
        }

        private readonly float _deltaTime;
        private EntityCommandBuffer.ParallelWriter _commandBuffer;

        [BurstCompile]
        private void Execute(ZombieRiseAspect zombieRiseAspect, [EntityIndexInQuery] int sortKey)
        {
            zombieRiseAspect.Rise(_deltaTime);

            if (!zombieRiseAspect.IsAboveGround)
            {
                return;
            }
            
            zombieRiseAspect.SetAtGroundLevel();

            _commandBuffer.SetComponentEnabled<ZombieWalkProperties>(sortKey, zombieRiseAspect.Entity, true);
            _commandBuffer.RemoveComponent<ZombieRiseRate>(sortKey, zombieRiseAspect.Entity);
        }
    }
}