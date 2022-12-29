using Components;
using Unity.Entities;
using Unity.Mathematics;

namespace Authoring
{
    public class GraveyardBaker : Baker<GraveyardMono>
    {
        public override void Bake(GraveyardMono authoring)
        {
            AddComponent(new GraveyardProperties
            {
                FieldDimension = authoring.FieldDimensions,
                TombstonesSpawnAmount = authoring.TombstonesSpawnAmount,
                TombstonePrefab = GetEntity(authoring.TombstonePrefab)
            });
            
            AddComponent(new GraveyardRandom
            {
                RandomValue = Random.CreateFromIndex(authoring.Seed)
            });
            
            AddComponent(new ZombieSpawnPoint { });
        }
    }
}