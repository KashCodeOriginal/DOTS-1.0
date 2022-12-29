using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Components
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        
        private readonly RefRO<GraveyardProperties> _graveyardProperties;

        private readonly RefRW<GraveyardRandom> _graveyardRandom;
        
        private readonly RefRW<ZombieSpawnPoint> _zombieSpawnPoint; 

        private const float TOMBSTONE_SAFETY_RADIUS = 200f;

        public int TombstonesSpawnAmount => _graveyardProperties.ValueRO.TombstonesSpawnAmount;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

        private float3 minCorner => _transformAspect.LocalPosition - HalfDimensions;
        private float3 maxCorner => _transformAspect.LocalPosition + HalfDimensions;

        public NativeArray<float3> ZombieSpawnPoints
        {
            get => _zombieSpawnPoint.ValueRO.SpawnPointPosition;
            set => _zombieSpawnPoint.ValueRW.SpawnPointPosition = value;
        }

        private float3 HalfDimensions => new float3()
        {
            x = _graveyardProperties.ValueRO.FieldDimension.x / 2,
            y = 0f,
            z = _graveyardProperties.ValueRO.FieldDimension.y / 2
        };

        public LocalTransform GetRandomTombstonePosition()
        {
            return new LocalTransform()
            {
                Position = GetRandomPosition(),
                Rotation = GetRandomRotation(),
                Scale = GetRandomScale(),
            }; 
        }

        private float3 GetRandomPosition()
        {
            float3 randomPosition = new float3();

            do
            {
                randomPosition = _graveyardRandom.ValueRW.RandomValue.NextFloat3(minCorner, maxCorner);
                randomPosition.y = 1f;
            }
            while (math.distancesq(_transformAspect.LocalPosition, randomPosition) < TOMBSTONE_SAFETY_RADIUS); 
            
            return randomPosition;
        }
        
        private quaternion GetRandomRotation()
        {
            return quaternion.RotateY(_graveyardRandom.ValueRW.RandomValue.NextFloat(-math.PI, math.PI));
        }
        
        private float GetRandomScale()
        {
            return _graveyardRandom.ValueRW.RandomValue.NextFloat(0.5f, 1f);
        }
    }
}