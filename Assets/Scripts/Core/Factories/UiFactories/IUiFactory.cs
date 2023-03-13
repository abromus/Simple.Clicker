using Clicker.Game.Screens;
using UnityEngine;

namespace Clicker.Core.Factories
{
    public interface IUiFactory
    {
        public UiFactoryType UiFactoryType { get; }

        public MonoBehaviour Create(BaseOptions options, Transform container);
    }
}
