using System;

namespace Clicker.Core.Services
{
    public interface ISceneLoader
    {
        public void Load(string name, Action success);
    }
}
