using Unity.Mathematics;

namespace Extensions
{
    public static class Extensions
    {
        public static float GetRotationForTargetAngle(this float3 position, float3 target)
        {
            return math.atan2(target.y - position.y, target.x - position.x) + math.PI;
        }
    }
}