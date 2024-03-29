﻿using Components.Graveyard;
using Components.Zombie.Spawn;
using Unity.Entities;
using Unity.Mathematics;

namespace Authoring.Graveyard
{
    public class GraveyardBaker : Baker<GraveyardMono>
    {
        public override void Bake(GraveyardMono authoring)
        {
            AddComponent(new GraveyardProperties
            {
                FieldDimension = authoring.FieldDimensions,
                TombstonesSpawnAmount = authoring.TombstonesSpawnAmount,
                TombstonePrefab = GetEntity(authoring.TombstonePrefab),
                ZombiePrefab = GetEntity(authoring.ZombiePrefab),
                ZombieSpawnInterval = authoring.ZombieSpawnInterval
            });
            
            AddComponent(new GraveyardRandom
            {
                RandomValue = Random.CreateFromIndex(authoring.Seed)
            });
            
            AddComponent(new ZombieSpawnPoint());
            AddComponent(new ZombieSpawnTimer());
        }
    }
}