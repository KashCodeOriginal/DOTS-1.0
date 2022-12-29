using Unity.Mathematics;
using UnityEngine;

namespace Authoring
{
    public class GraveyardMono : MonoBehaviour
    {
        [SerializeField] private float2 _fieldDimensions;
        
        [SerializeField] private int _tombstonesSpawnAmount;
        
        [SerializeField] private GameObject _tombstonePrefab;
        
        [SerializeField] private uint _seed;
        
        public float2 FieldDimensions => _fieldDimensions;

        public int TombstonesSpawnAmount => _tombstonesSpawnAmount;

        public GameObject TombstonePrefab => _tombstonePrefab;

        public uint Seed => _seed;
    }
}