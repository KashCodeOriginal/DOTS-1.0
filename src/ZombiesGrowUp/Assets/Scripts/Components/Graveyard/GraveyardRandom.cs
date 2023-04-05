using Unity.Entities;
using Unity.Mathematics;

namespace Components.Graveyard
{
    public struct GraveyardRandom : IComponentData
    {
        public Random RandomValue;
    }
}