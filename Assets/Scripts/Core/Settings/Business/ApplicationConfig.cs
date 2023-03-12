using UnityEngine;

namespace Clicker.Core
{
    [CreateAssetMenu(fileName = "ApplicationConfig", menuName = "Settings/ApplicationConfig")]
    public class ApplicationConfig : ScriptableObject
    {
        [SerializeField] private int _targetFrameRate;

        public int TargetFrameRate => _targetFrameRate;
    }
}
