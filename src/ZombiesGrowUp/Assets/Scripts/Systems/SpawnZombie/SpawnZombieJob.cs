using Components.Zombie.Heading;
using Extensions;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using GraveyardAspect = Components.Graveyard.GraveyardAspect;

namespace Systems.SpawnZombie
{
    [BurstCompile]
    public partial struct SpawnZombieJob : IJobEntity
    {
        public SpawnZombieJob(float deltaTime, EntityCommandBuffer ecb) : this()
        {
            _deltaTime = deltaTime;
            _ecb = ecb;
        }

        private readonly float _deltaTime;

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

            var zombieHeading = newZombieSpawnPoint.Position.GetRotationForTargetAngle(graveyardAspect.Position);
            
            _ecb.SetComponent(zombieInstance, new ZombieHeading()
            {
                Heading = zombieHeading
            });
        }
    }
}