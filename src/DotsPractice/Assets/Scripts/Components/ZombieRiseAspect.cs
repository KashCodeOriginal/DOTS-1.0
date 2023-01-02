using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Components
{
    public readonly partial struct ZombieRiseAspect : IAspect
    {
        public readonly Entity Entity;
        
        private readonly TransformAspect _transformAspect;
        private readonly RefRO<ZombieRiseRate> _zombieRiseRate;
        
        public bool IsAboveGround => _transformAspect.LocalPosition.y >= 0f;

        public void Rise(float deltaTime)
        {
            _transformAspect.LocalPosition += math.up() * _zombieRiseRate.ValueRO.SpawnRateValue * deltaTime;
        }

        public void SetAtGroundLevel()
        {
            _transformAspect.LocalPosition =
                new float3(_transformAspect.LocalPosition.x, 0f, _transformAspect.LocalPosition.z);
        }
    }
}