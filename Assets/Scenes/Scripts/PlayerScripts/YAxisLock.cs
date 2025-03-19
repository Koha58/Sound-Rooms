using UnityEngine;
using Cinemachine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Cinemachineのカメラシステムにおいて、指定された軸を固定する拡張機能
/// </summary>
[ExecuteInEditMode]  // 編集モードでも実行されるようにする
[SaveDuringPlay]    // プレイ中でも変更を保存
[AddComponentMenu("")] // メニューに表示しないようにする（非表示）
public class YAxisLock : CinemachineExtension
{
    // X, Y, Z軸のロック状態を保持するフラグ
    public bool x_islocked, y_islocked, z_islocked;
    // ロックする位置（最終的にカメラの位置がこの座標に固定される）
    public Vector3 lockPosition;

    // Cinemachineのパイプライン内の各ステージが呼ばれた後に実行されるコールバック
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        // カメラのボディ部分（位置）に対してのみ処理を行う
        if (stage == CinemachineCore.Stage.Body)
        {
            var newPos = state.RawPosition;  // 現在のカメラ位置を取得
            // 各軸に対してロックする場合、指定した位置に変更
            if (x_islocked) newPos.x = lockPosition.x;
            if (y_islocked) newPos.y = lockPosition.y;
            if (z_islocked) newPos.z = lockPosition.z;
            state.RawPosition = newPos;  // カメラ位置を更新
        }
    }
}

#if UNITY_EDITOR
// Unityエディタでのインスペクター表示をカスタマイズ
[CustomEditor(typeof(YAxisLock))]
public class LockAxisCameraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // YAxisLockコンポーネントを対象にインスペクターGUIを描画
        var lockAxisCamera = target as YAxisLock;

        // 軸のロックを選択するためのUIを表示
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUIUtility.labelWidth = 10;  // ラベルの幅を調整
            EditorGUILayout.LabelField("固定する軸");
            lockAxisCamera.x_islocked = EditorGUILayout.Toggle("X", lockAxisCamera.x_islocked);  // X軸のロック
            lockAxisCamera.y_islocked = EditorGUILayout.Toggle("Y", lockAxisCamera.y_islocked);  // Y軸のロック
            lockAxisCamera.z_islocked = EditorGUILayout.Toggle("Z", lockAxisCamera.z_islocked);  // Z軸のロック
        }

        // 固定する座標（位置）の設定を表示
        EditorGUILayout.LabelField("固定する座標");
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUIUtility.labelWidth = 10;  // ラベルの幅を調整
            lockAxisCamera.lockPosition.x = EditorGUILayout.FloatField("X", lockAxisCamera.lockPosition.x);  // X座標
            lockAxisCamera.lockPosition.y = EditorGUILayout.FloatField("Y", lockAxisCamera.lockPosition.y);  // Y座標
            lockAxisCamera.lockPosition.z = EditorGUILayout.FloatField("Z", lockAxisCamera.lockPosition.z);  // Z座標
        }
    }
}
#endif