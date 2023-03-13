using Clicker.Game.Screens;
using UnityEngine;

namespace Clicker.Core.Factories
{
    public abstract class UiFactory : MonoBehaviour, IUiFactory
    {
        public abstract UiFactoryType UiFactoryType { get; }

        public abstract MonoBehaviour Create(BaseOptions options, Transform container);
    }
}
