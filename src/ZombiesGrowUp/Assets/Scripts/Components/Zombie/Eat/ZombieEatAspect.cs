using Components.Brain;
using Components.Zombie.Heading;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Components.Zombie.Eat
{
    public readonly partial struct ZombieEatAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRW<ZombieTimer> _zombieTimer;
        private readonly RefRO<ZombieEatProperties> _zombieEatProperties;
        private readonly RefRO<ZombieHeading> _zombieHeading;
        
        public float ZombieDamagePerSecond => _zombieEatProperties.ValueRO.DamagePerSecond;
        public float ZombieEatAmplitude => _zombieEatProperties.ValueRO.Amplitude;
        public float ZombieEatFrequency => _zombieEatProperties.ValueRO.Frequency;
        public float ZombieHeading => _zombieHeading.ValueRO.Heading;
        
        public float ZombieTimer 
        {
            get => _zombieTimer.ValueRO.Timer;
            set => _zombieTimer.ValueRW.Timer = value;
        }

        public void Eat(float deltaTime,
            EntityCommandBuffer.ParallelWriter commandBuffer,
            int sortKey,
            Entity entity)
        {
            ZombieTimer += deltaTime;
            
            var eatAngle = ZombieEatAmplitude * math.sin(ZombieEatFrequency * ZombieTimer);
            
            _transformAspect.LocalRotation = quaternion.Euler(eatAngle, ZombieHeading, 0);
            
            var eatDamage = ZombieDamagePerSecond * deltaTime;
            
            var currBrainDamage = new BrainDamageBufferElement
            {
                Value = eatDamage
            };
            
            commandBuffer.AppendToBuffer(sortKey, entity, currBrainDamage);
        }

        public bool IsInEatingRange(float3 brainPosition, float brainRadiusSq)
        {
            return math.distancesq(brainPosition, _transformAspect.WorldPosition) <= brainRadiusSq - 1;
        }
    }
}