using UnityEngine;

namespace Clicker.Core.Services
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CoreSceneController _coreSceneController;

        private void Awake()
        {
            _coreSceneController.CreateGame();
        }
    }
}
