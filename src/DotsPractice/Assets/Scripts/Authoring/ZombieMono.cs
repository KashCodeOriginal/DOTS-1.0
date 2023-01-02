using UnityEngine;

namespace Authoring
{
    public class ZombieMono : MonoBehaviour
    {
        [SerializeField] private float _zombieRiseInterval;
        [SerializeField] private float _zombieWalkSpeed;
        [SerializeField] private float _zombieWalkAmplitude;
        [SerializeField] private float _zombieWalkFrequency;
        
        public float ZombieRiseInterval => _zombieRiseInterval;
        public float ZombieWalkSpeed => _zombieWalkSpeed;
        public float ZombieWalkAmplitude => _zombieWalkAmplitude;
        public float ZombieWalkFrequency => _zombieWalkFrequency;
    }
}