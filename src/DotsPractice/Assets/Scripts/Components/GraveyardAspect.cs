using DefaultNamespace;
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
        
        private readonly RefRW<ZombieSpawnPoint> _zombieSpawnPoints;
        
        private readonly RefRW<ZombieSpawnTimer> _zombieSpawnTimer;

        private const float TOMBSTONE_SAFETY_RADIUS = 200f;

        public int TombstonesSpawnAmount => _graveyardProperties.ValueRO.TombstonesSpawnAmount;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;
        public Entity ZombiePrefab => _graveyardProperties.ValueRO.ZombiePrefab;
        
        public float ZombieSpawnRate => _graveyardProperties.ValueRO.ZombieSpawnInterval;

        public LocalTransform GetZombieSpawnPoint()
        {
            var position = GetRandomZombieSpawnPoint();
            
            return new LocalTransform()
            {
                Position = position,
                Rotation = quaternion.RotateY(position.GetRotationForTargetAngle(_transformAspect.LocalPosition)),
                Scale = 1f
            };
        }

        private float3 minCorner => _transformAspect.LocalPosition - HalfDimensions;
        private float3 maxCorner => _transformAspect.LocalPosition + HalfDimensions;
        
        public bool TimeToSpawnZombie => ZombieSpawnTimer <= 0f;

        public NativeArray<float3> ZombieSpawnPoints
        {
            get => _zombieSpawnPoints.ValueRO.SpawnPointPosition;
            set => _zombieSpawnPoints.ValueRW.SpawnPointPosition = value;
        }

        public float ZombieSpawnTimer
        {
            get => _zombieSpawnTimer.ValueRO.SpawnTime;
            set => _zombieSpawnTimer.ValueRW.SpawnTime = value;
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
        
        private float3 GetRandomZombieSpawnPoint()
        {
            return ZombieSpawnPoints[_graveyardRandom.ValueRW.RandomValue.NextInt(ZombieSpawnPoints.Length)];
        }
    }
}