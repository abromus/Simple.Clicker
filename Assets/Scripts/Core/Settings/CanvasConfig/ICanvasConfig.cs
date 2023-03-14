using UnityEngine;
using UnityEngine.UI;

namespace Clicker.Core.Settings
{
    public interface ICanvasConfig : IUiConfig
    {
        public string Name { get; }

        public RenderMode RenderMode { get; }

        public CanvasScaler.ScaleMode ScaleMode { get; }

        public Vector2 ReferenceResolution { get; }

        public float MatchWidthOrHeight { get; }

        public float ReferencePixelsPerUnit { get; }
    }
}
