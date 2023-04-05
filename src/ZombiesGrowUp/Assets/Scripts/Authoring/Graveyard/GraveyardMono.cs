using Unity.Mathematics;
using UnityEngine;

namespace Authoring.Graveyard
{
    public class GraveyardMono : MonoBehaviour
    {
        [SerializeField] private float2 _fieldDimensions;
        
        [SerializeField] private int _tombstonesSpawnAmount;
        
        [SerializeField] private GameObject _tombstonePrefab;
        
        [SerializeField] private GameObject _zombiePrefab;
        
        [SerializeField] private uint _seed;

        [SerializeField] private float _zombieSpawnInterval;

        public float2 FieldDimensions => _fieldDimensions;
        public int TombstonesSpawnAmount => _tombstonesSpawnAmount;
        public GameObject TombstonePrefab => _tombstonePrefab;
        public uint Seed => _seed;
        public GameObject ZombiePrefab => _zombiePrefab;
        public float ZombieSpawnInterval => _zombieSpawnInterval;
    }
}