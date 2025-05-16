using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;  // シーンの操作に必要な名前空間
using UnityEditor;  // Unityエディタの機能を使用するための名前空間

/// <summary>
/// エディタ拡張：開発環境で特定のシーンを簡単に開くためのクラス
/// </summary>
public static class EditorSceneLauncher  // エディタ拡張用の静的クラス
{
    // メニューに「Launcher/StartScene」を追加し、シーン「StartScene.unity」を開く
    [MenuItem("Launcher/StartScene", priority = 0)]
    public static void OpenGameScene()
    {
        // 指定したパスのシーンを開く
        OpenSceneWithSaveCheck("Assets/Scenes/StartScene.unity");
    }

    // メニューに「Launcher/StageSelectScene」を追加し、シーン「StageSelectScene.unity」を開く
    [MenuItem("Launcher/StageSelectScene", priority = 1)]
    public static void OpenStageSelectScene()
    {
        // 指定したパスのシーンを開く
        OpenSceneWithSaveCheck("Assets/Scenes/StageSelectScene.unity");
    }

    // メニューに「Launcher/GetRecorderScene」を追加し、シーン「GetRecorder.unity」を開く
    [MenuItem("Launcher/GetRecorderScene", priority = 2)]
    public static void OpenGetRecorderScene()
    {
        // 指定したパスのシーンを開く
        OpenSceneWithSaveCheck("Assets/Scenes/GetRecorder.unity");
    }

    // メニューに「Launcher/TutorialScene」を追加し、シーン「TutorialScene.unity」を開く
    [MenuItem("Launcher/TutorialScene", priority = 3)]
    public static void OpenTutorialScene()
    {
        // 指定したパスのシーンを開く
        OpenSceneWithSaveCheck("Assets/Scenes/TutorialScene.unity");
    }

    // メニューに「Launcher/Stage1Scene」を追加し、シーン「Stage1.unity」を開く
    [MenuItem("Launcher/Stage1Scene", priority = 4)]
    public static void OpenStage1Scene()
    {
        // 指定したパスのシーンを開く
        OpenSceneWithSaveCheck("Assets/Scenes/Stage1.unity");
    }

    // メニューに「Launcher/Stage2Scene」を追加し、シーン「Stage2.unity」を開く
    [MenuItem("Launcher/Stage2Scene", priority = 5)]
    public static void OpenStage2Scene()
    {
        // 指定したパスのシーンを開く
        OpenSceneWithSaveCheck("Assets/Scenes/Stage2.unity");
    }

    // メニューに「Launcher/GameClearScene」を追加し、シーン「GameClearScene.unity」を開く
    [MenuItem("Launcher/GameClearScene", priority = 6)]
    public static void OpenGameClearScene()
    {
        // 指定したパスのシーンを開く
        OpenSceneWithSaveCheck("Assets/Scenes/GameClearScene.unity");
    }

    // メニューに「Launcher/GameOverScene」を追加し、シーン「GameOverScene.unity」を開く
    [MenuItem("Launcher/GameOverScene", priority = 7)]
    public static void OpenGameOverTutorialScene()
    {
        // 指定したパスのシーンを開く
        OpenSceneWithSaveCheck("Assets/Scenes/GameOverScene.unity");
    }

    /// <summary>
    /// シーンを開く前に、現在のシーンに未保存の変更がある場合は保存を促し、
    /// 指定されたパスのシーンを開く。
    /// </summary>
    /// <param name="scenePath">開きたいシーンのプロジェクト内パス</param>
    static void OpenSceneWithSaveCheck(string scenePath)
    {
        // 指定されたシーンファイルが存在するか確認
        if (!System.IO.File.Exists(scenePath))
        {
            Debug.LogError($"シーンが見つかりません: {scenePath}");
            return;
        }

        // 現在のシーンに変更がある場合、保存するかどうかをユーザーに確認
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            // シーンを単一モードで開く（現在のシーンを閉じて指定のシーンに切り替える）
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }
    }
}
