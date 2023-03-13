using Clicker.Core.World;

namespace Clicker.Core
{
    public static class EcsUtils
    {
        public static T CreateFilter<T>(this IWorld world) where T : class
        {
            var levelUpdateFilterType = typeof(T);

            return world.GetFilter(levelUpdateFilterType) as T;
        }
    }
}
