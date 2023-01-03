using UnityEngine;

namespace Authoring.Zombie
{
    public class ZombieMono : MonoBehaviour
    {
        [SerializeField] private float _zombieRiseInterval;
        
        [SerializeField] private float _zombieWalkSpeed;
        [SerializeField] private float _zombieWalkAmplitude;
        [SerializeField] private float _zombieWalkFrequency;
        
        [SerializeField] private float _zombieDamagePerSecond;


        [SerializeField] private float _zombieEatAmplitude;
        [SerializeField] private float _zombieEatFrequency;

        public float ZombieRiseInterval => _zombieRiseInterval;
        
        public float ZombieWalkSpeed => _zombieWalkSpeed;
        public float ZombieWalkAmplitude => _zombieWalkAmplitude;
        public float ZombieWalkFrequency => _zombieWalkFrequency;
        
        public float ZombieDamagePerSecond => _zombieDamagePerSecond;
        public float ZombieEatAmplitude => _zombieEatAmplitude;
        public float ZombieEatFrequency => _zombieEatFrequency;
    }
}