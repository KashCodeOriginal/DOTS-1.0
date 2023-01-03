using Unity.Entities;
using Unity.Mathematics;

namespace Components.Graveyard
{
    public struct GraveyardProperties : IComponentData
    {
        public float2 FieldDimension;
        public int TombstonesSpawnAmount;
        public Entity TombstonePrefab;
        public Entity ZombiePrefab;
        public float ZombieSpawnInterval;
    }
}
