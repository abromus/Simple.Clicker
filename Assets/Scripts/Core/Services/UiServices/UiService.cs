using UnityEngine;

namespace Clicker.Core.Services
{
    public abstract class UiService : MonoBehaviour, IUiService
    {
        public abstract UiServiceType UiServiceType { get; }
    }
}
