using Components.Zombie.Heading;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Components.Zombie.Walk
{
    public readonly partial struct ZombieWalkAspect : IAspect
    {
        public readonly Entity Entity;
        
        private readonly TransformAspect _transformAspect;
        
        private readonly RefRW<ZombieTimer> _zombieTimer;
        private readonly RefRO<ZombieHeading> _zombieHeading;
        private readonly RefRO<ZombieWalkProperties> _zombieWalkingProps;

        private float _walkSpeed => _zombieWalkingProps.ValueRO.WalkSpeed;
        private float _walkAmplitude => _zombieWalkingProps.ValueRO.WalkAmplitude;
        private float _walkFrequency => _zombieWalkingProps.ValueRO.WalkFrequency;

        private float _heading => _zombieHeading.ValueRO.Heading;

        private float _timer
        {
            get => _zombieTimer.ValueRO.Timer;
            set => _zombieTimer.ValueRW.Timer = value;
        }

        public void Walk(float deltaTime)
        {
            _timer += deltaTime;
            
            _transformAspect.LocalPosition += _transformAspect.Forward * _walkSpeed * deltaTime;
            
            var swayAngle = _walkAmplitude * math.sin(_walkFrequency * _timer);
            _transformAspect.LocalRotation = quaternion.Euler(0, _heading, swayAngle);
        }
        
        public bool IsStoppingDistanceReached(float3 brainPosition, float brainRadius)
        {
            return math.distancesq(brainPosition, _transformAspect.LocalPosition) <= brainRadius;
        }
    }
}