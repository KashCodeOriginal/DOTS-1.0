using Unity.Entities;
using Unity.Transforms;

namespace Components.Brain
{
    public readonly partial struct BrainAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRW<BrainHealth> _health;
        private readonly DynamicBuffer<BrainDamageBufferElement> _damageBuffer;
        
        public void ApplyDamage()
        {
            foreach (var brainDamageBufferElement in _damageBuffer)
            {
                _health.ValueRW.CurrentHealth -= brainDamageBufferElement.Value;
            }

            _damageBuffer.Clear();

            var localTransform = _transformAspect.LocalTransform;
            localTransform.Scale = (_health.ValueRO.CurrentHealth / _health.ValueRO.MaxHealth) * 10f;

            _transformAspect.LocalTransform = localTransform;
        }
    }
}