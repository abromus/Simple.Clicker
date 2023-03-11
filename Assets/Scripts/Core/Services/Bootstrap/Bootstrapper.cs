using UnityEngine;

namespace Clicker.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CoreSceneController _coreSceneController;

        private void Awake()
        {
            _coreSceneController.CreateGame();
        }
    }
}
