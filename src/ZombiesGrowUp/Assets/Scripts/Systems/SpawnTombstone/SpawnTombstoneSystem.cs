using Components.Graveyard;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using GraveyardAspect = Components.Graveyard.GraveyardAspect;

namespace Systems.SpawnTombstone
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraveyardProperties>();
        }
        
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardProperties>();
            var graveyard = SystemAPI.GetAspectRW<GraveyardAspect>(graveyardEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var spawnPoints = new NativeList<float3>(Allocator.Temp);
            var tombstoneOffset = new float3(0f, -4f, 1f);

            for (int i = 0; i < graveyard.TombstonesSpawnAmount; i++)
            {
                var entity = ecb.Instantiate(graveyard.TombstonePrefab);

                var tombstoneTransform = graveyard.GetRandomTombstonePosition();
                
                ecb.SetComponent(entity, new LocalTransform
                {
                    Position = tombstoneTransform._Position,
                    Rotation = tombstoneTransform._Rotation,
                    Scale = tombstoneTransform._Scale
                });
                
                var zombieSpawnPoint = tombstoneTransform._Position + tombstoneOffset;
                
                spawnPoints.Add(zombieSpawnPoint);
            }

            graveyard.ZombieSpawnPoints = spawnPoints.ToArray(Allocator.Persistent);
            ecb.Playback(state.EntityManager);
        }
    }
}