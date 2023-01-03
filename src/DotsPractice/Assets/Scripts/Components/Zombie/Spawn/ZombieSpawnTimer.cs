using Unity.Entities;

namespace Components.Zombie.Spawn
{
    public struct ZombieSpawnTimer : IComponentData
    {
        public float SpawnTime;
    }
}