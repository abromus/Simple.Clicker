using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Game
{
    public class Screen : MonoBehaviour, IScreen
    {
        public virtual ScreenType ScreenType { get; }

        public virtual void Init(Dictionary<string, object> data) { }

        public virtual void Show(Dictionary<string, object> data)
        {
            Init(data);
        }
    }
}
