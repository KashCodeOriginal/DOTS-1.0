using Unity.Mathematics;

namespace Extensions
{
    public static class Extensions
    {
        public static float GetRotationForTargetAngle(this float3 position, float3 target)
        {
            var x = position.x - target.x;
            var y = position.z - target.z;
            return math.atan2(x, y) + math.PI;
        }
    }
}