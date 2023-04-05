using UnityEngine;

namespace Authoring.Brain
{
    public class BrainMono : MonoBehaviour
    {
        [SerializeField] private float _health;

        public float Health => _health;
    }
}
