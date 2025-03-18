using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

public static class EditorSceneLauncher
{
    [MenuItem("Launcher/StartScene", priority = 0)]
    public static void OpenGameScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/StartScene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/StageSelectScene", priority = 0)]
    public static void OpenStageSelectScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/StageSelectScene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/TutorialScene", priority = 0)]
    public static void OpenTutorialScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/TutorialScene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/TutorialClearScene", priority = 0)]
    public static void OpenTutorialClearScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/TutorialClear.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/GameOver_TutorialScene", priority = 0)]
    public static void OpenGameOverTutorialScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/GameOver_Tutorial.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/Stage1Scene", priority = 0)]
    public static void OpenStage1Scene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Stage1.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/Stage1ClearScene", priority = 0)]
    public static void OpenStage1ClearScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Stage1Clear.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/GameOver_Stage1", priority = 0)]
    public static void OpenGameOver_Stage1Scene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/GameOver_Stage1.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/GameScene", priority = 0)]
    public static void OpenGameSceneScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/GameScene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/GameClear", priority = 0)]
    public static void OpenGameClear()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/GameClear.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/GameOver", priority = 0)]
    public static void OpenGameOverClear()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/GameOver.unity", OpenSceneMode.Single);
    }
}
