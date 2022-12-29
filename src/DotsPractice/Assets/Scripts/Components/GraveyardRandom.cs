using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct GraveyardRandom : IComponentData
    {
        public Random RandomValue;
    }
}