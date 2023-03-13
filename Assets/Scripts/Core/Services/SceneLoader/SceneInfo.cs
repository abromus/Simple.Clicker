using System;

namespace Clicker.Core.Services
{
    public sealed class SceneInfo
    {
        public string Name { get; }

        public Action Success { get; }

        public SceneInfo(string name, Action success)
        {
            Name = name;
            Success = success;
        }
    }
}
