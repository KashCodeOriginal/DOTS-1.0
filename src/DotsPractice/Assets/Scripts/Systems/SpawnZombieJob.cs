using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
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