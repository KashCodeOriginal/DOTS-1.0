using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Components
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
        }
    }
}