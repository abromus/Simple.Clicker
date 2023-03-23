using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Clicker.Core.Services
{
    public sealed class SceneLoader : ISceneLoader, IService
    {
        public void Load(string name, Action success)
        {
            LoadScene(name, success);
        }

        private async void LoadScene(string name, Action success = null)
        {
            if (SceneManager.GetActiveScene().name != name)
            {
                var operation = SceneManager.LoadSceneAsync(name);

                await UniTask.WaitUntil(() => operation.isDone);
            }

            success.SafeInvoke();
        }
    }
}
