using Unity.Entities;

namespace Systems.SpawnZombie
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
}