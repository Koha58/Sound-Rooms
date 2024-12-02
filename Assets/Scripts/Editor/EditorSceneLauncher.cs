using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

public static class EditorSceneLauncher
{
    [MenuItem("Launcher/GameScene", priority = 0)]
    public static void OpenGameScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/GameScene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/TutorialScene", priority = 0)]
    public static void OpenSampleScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/TutorialScene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/StartScene", priority = 0)]
    public static void OpenTitleScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/StartScene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/TestScene", priority = 0)]
    public static void OpenTestScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Test/TestScene.unity", OpenSceneMode.Single);
    }
}
