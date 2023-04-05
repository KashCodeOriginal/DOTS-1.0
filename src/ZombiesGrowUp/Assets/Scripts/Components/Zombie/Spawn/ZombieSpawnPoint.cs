using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Components.Zombie.Spawn
{
    public struct ZombieSpawnPoint : IComponentData
    {
        public NativeArray<float3> SpawnPointPosition;
    }
}