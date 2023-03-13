﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Clicker.Core.Services
{
    [InitializeOnLoad]
    public sealed class BootstrapSceneManagement
    {
        static BootstrapSceneManagement()
        {
            OnSceneListChanged();

            EditorBuildSettings.sceneListChanged += OnSceneListChanged;
        }

        private static void OnSceneListChanged()
        {
            if (EditorBuildSettings.scenes.Length == 0)
                return;

            var startSceneIndex = 0;
            var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[startSceneIndex].path);

            EditorSceneManager.playModeStartScene = scene;
        }
    }
}
#endif
