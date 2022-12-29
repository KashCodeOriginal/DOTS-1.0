using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct ZombieSpawnPoint : IComponentData
    {
        public NativeArray<float3> SpawnPointPosition;
    }
}