using UnityEngine;
using UnityEngine.UI;

namespace Clicker.Core
{
    [CreateAssetMenu(fileName = "CanvasConfig", menuName = "Settings/CanvasConfig")]
    public class CanvasConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private RenderMode _renderMode;
        [SerializeField] private CanvasScaler.ScaleMode _scaleMode;
        [SerializeField] private Vector2 _referenceResolution;
        [SerializeField] private float _matchWidthOrHeight;
        [SerializeField] private float _referencePixelsPerUnit;

        public string Name => _name;

        public RenderMode RenderMode => _renderMode;

        public CanvasScaler.ScaleMode ScaleMode => _scaleMode;

        public Vector2 ReferenceResolution => _referenceResolution;

        public float MatchWidthOrHeight => _matchWidthOrHeight;

        public float ReferencePixelsPerUnit => _referencePixelsPerUnit;
    }
}
