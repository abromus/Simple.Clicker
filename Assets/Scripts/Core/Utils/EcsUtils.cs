using Clicker.Core.World;

namespace Clicker.Core
{
    public static class EcsUtils
    {
        public static T CreateFilter<T>(this IWorld world) where T : class
        {
            var filterType = typeof(T);

            return world.GetFilter(filterType) as T;
        }
    }
}
