using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct SpawnZombieSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            
        }

        public void OnDestroy(ref SystemState state)
        {
            
        }

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            new SpawnZombieJob(deltaTime, ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)).Run();
        }
    }
    
    [BurstCompile]
    public partial struct SpawnZombieJob : IJobEntity
    {
        public SpawnZombieJob(float deltaTime, EntityCommandBuffer ecb) : this()
        {
            _deltaTime = deltaTime;
            _ecb = ecb;
        }

        private float _deltaTime;

        private EntityCommandBuffer _ecb;

        private void Execute(GraveyardAspect graveyardAspect)
        {
            graveyardAspect.ZombieSpawnTimer -= _deltaTime;

            if (!graveyardAspect.TimeToSpawnZombie)
            {
                return;
            }

            if (graveyardAspect.ZombieSpawnPoints.Length == 0)
            {
                return;
            }
            
            graveyardAspect.ZombieSpawnTimer = graveyardAspect.ZombieSpawnRate;
            var zombieInstance = _ecb.Instantiate(graveyardAspect.ZombiePrefab);

            var newZombieSpawnPoint = graveyardAspect.GetZombieSpawnPoint();
            
            _ecb.SetComponent(zombieInstance, new LocalTransform
            {
                Position = newZombieSpawnPoint.Position,
                Rotation = newZombieSpawnPoint.Rotation,
                Scale = newZombieSpawnPoint.Scale
            });
        }
    }
}