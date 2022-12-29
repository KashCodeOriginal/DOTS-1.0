using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct GraveyardProperties : IComponentData
    {
        public float2 FieldDimension;
        public int TombstonesSpawnAmount;
        public Entity TombstonePrefab;
    }
}
