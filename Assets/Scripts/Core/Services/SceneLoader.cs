using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Clicker.Core
{
    public class SceneLoader
    {
        public void Load(string name, Action success = null)
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
