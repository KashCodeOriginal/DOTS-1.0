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

        public int TombstonesSpawnAmount => _graveyardProperties.ValueRO.TombstonesSpawnAmount;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

        private float3 minCorner => _transformAspect.LocalPosition - HalfDimensions;
        private float3 maxCorner => _transformAspect.LocalPosition + HalfDimensions;

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
                Rotation = quaternion.identity,
                Scale = 1,
            }; 
        }

        private float3 GetRandomPosition()
        {
            float3 randomPosition = new float3();

            randomPosition = _graveyardRandom.ValueRW.RandomValue.NextFloat3(minCorner, maxCorner);

            return randomPosition;
        }
    }
}