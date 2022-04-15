using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneTools {
    #region Editor Scenes Menu
    public static string[] ScenePaths = { "Assets/Game/Scenes/Logo.unity",
                                          "Assets/Game/Scenes/Home.unity",
                                          "Assets/Game/Scenes/Game.unity",
                                          "Assets/Game/Scenes/Edit.unity",};

    [MenuItem("Game/Play %K", false, 0)]
    private static void PlayGame() {
        OpenLogoScene();
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Game/Scenes/Logo Scene %L", false, 1)]
    private static void OpenLogoScene() {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(ScenePaths[0]);
    }

    [MenuItem("Game/Scenes/Home Scene %H", false, 1)]
    private static void OpenSplashScene() {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(ScenePaths[1]);
    }

    [MenuItem("Game/Scenes/Game Scene %G", false, 1)]
    private static void OpenGamePlayScene() {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(ScenePaths[2]);
    }

    [MenuItem("Game/Scenes/Game Edit %E", false, 1)]
    private static void OpenEditScene() {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(ScenePaths[3]);
    }
    #endregion
}
