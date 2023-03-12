using Clicker.Core;
using UnityEngine;

namespace Clicker.Game
{
    public class Screen : MonoBehaviour, IScreen
    {
        public virtual ScreenType ScreenType { get; }

        public virtual void Init(GameManagement game) { }
    }
}
