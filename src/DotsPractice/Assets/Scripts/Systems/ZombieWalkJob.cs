using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    public partial struct ZombieWalkJob : IJobEntity
    {
        public ZombieWalkJob(float deltaTime) : this()
        {
            _deltaTime = deltaTime;
        }

        private readonly float _deltaTime;

        [BurstCompile]
        private void Execute(ZombieWalkAspect zombieWalkAspect)
        {
            zombieWalkAspect.Walk(_deltaTime);
        }
    }
}