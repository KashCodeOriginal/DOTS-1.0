using Components.Zombie.Eat;
using Components.Zombie.Walk;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using ZombieEatAspect = Components.Zombie.Eat.ZombieEatAspect;

namespace Systems.Eat
{
    [BurstCompile]
    public partial struct EatZombieJob : IJobEntity
    {
        public EatZombieJob(float deltaTime, EntityCommandBuffer.ParallelWriter commandBuffer, Entity entity, float brainRadiusSq) : this()
        {
            _deltaTime = deltaTime;
            _commandBuffer = commandBuffer;
            _entity = entity;
            _brainRadiusSq = brainRadiusSq;
        }

        private readonly float _deltaTime;
        private EntityCommandBuffer.ParallelWriter _commandBuffer;
        private readonly Entity _entity;
        private readonly float _brainRadiusSq;

        [BurstCompile]
        private void Execute(ZombieEatAspect eatAspect, [EntityIndexInQuery] int sortKey)
        {
            eatAspect.Eat(_deltaTime, _commandBuffer, sortKey, _entity);
            
            /*if (eatAspect.IsInEatingRange(float3.zero, _brainRadiusSq))
            {
                
            }
            else
            {
                _commandBuffer.SetComponentEnabled<ZombieEatProperties>(sortKey, _entity, false);
                _commandBuffer.SetComponentEnabled<ZombieWalkProperties>(sortKey, _entity, true);
            }*/
        }
    }
}