using UnityEngine;

namespace Clicker.Core.Settings
{
    [CreateAssetMenu(fileName = "ApplicationConfig", menuName = "Settings/ApplicationConfig")]
    public sealed class ApplicationConfig : ScriptableObject
    {
        [SerializeField] private int _targetFrameRate;

        public int TargetFrameRate => _targetFrameRate;
    }
}
