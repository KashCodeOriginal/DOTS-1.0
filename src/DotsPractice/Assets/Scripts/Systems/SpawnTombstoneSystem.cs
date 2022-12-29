using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
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
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}