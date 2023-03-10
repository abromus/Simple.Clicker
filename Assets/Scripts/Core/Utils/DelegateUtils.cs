using System;

namespace Clicker.Core
{
    public static class DelegateUtils
    {
        public static void SafeInvoke(this Action block)
        {
            block?.Invoke();
        }
    }
}
