#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

/// <summary>
/// エディタ拡張：巡回ポイントをランダムに配置するためのカスタムウィンドウ
/// シーン上の指定範囲に巡回ポイントを一括生成するツール
/// </summary>
public class RandomPatrolPointCreator : EditorWindow
{
    // 生成される巡回ポイントを格納する親オブジェクトの名前
    private string parentName = "EnemyPatrolPoints";

    // 巡回ポイントを生成する個数
    private int pointCount = 10;

    // 巡回ポイントを配置する範囲の中心座標（ワールド座標）
    private Vector3 centerPosition = Vector3.zero;

    // 巡回ポイントを配置する範囲のサイズ（XとZ方向の広がり）
    private Vector3 rangeSize = new Vector3(10, 0, 10);

    // ドラッグで指定された開始位置（スクリーン座標）
    private Vector2? dragStartPos = null;

    // ドラッグで指定された終了位置（スクリーン座標）
    private Vector2 dragEndPos;

    // 現在ドラッグ中かどうかを示すフラグ
    private bool isDragging = false;

    /// <summary>
    /// メニュー項目「Tools/Random Patrol Point Creator」としてエディタに表示される
    /// </summary>
    [MenuItem("Tools/Random Patrol Point Creator")]
    public static void ShowWindow()
    {
        // ウィンドウを開いて取得し、タイトルを設定
        var window = GetWindow<RandomPatrolPointCreator>("Random Patrol Point Creator");

        // シーンビューの描画イベントに登録（ドラッグ描画対応）
        SceneView.duringSceneGui += window.OnSceneGUI;
    }

    /// <summary>
    /// ウィンドウが閉じられたとき、イベント登録を解除する
    /// </summary>
    private void OnDestroy()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    /// <summary>
    /// エディタウィンドウのGUI描画処理
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("ランダム巡回ポイント生成ツール", EditorStyles.boldLabel);

        // 入力フィールドを描画
        parentName = EditorGUILayout.TextField("親オブジェクト名", parentName);
        pointCount = EditorGUILayout.IntField("ポイント数", pointCount);
        centerPosition = EditorGUILayout.Vector3Field("範囲中心位置", centerPosition);
        rangeSize = EditorGUILayout.Vector3Field("範囲サイズ", rangeSize);

        // 巡回ポイント生成ボタン
        if (GUILayout.Button("ランダム巡回ポイントを生成"))
        {
            CreateRandomPatrolPoints();
        }

        GUILayout.Space(10);
        GUILayout.Label("【Shift + 左クリックドラッグ】でシーンビューに範囲を作成・取得します。", EditorStyles.helpBox);
    }

    /// <summary>
    /// 巡回ポイントを範囲内にランダム生成する処理
    /// </summary>
    private void CreateRandomPatrolPoints()
    {
        // ポイント数が0以下の場合は警告を出して処理を中断
        if (pointCount <= 0)
        {
            Debug.LogWarning("ポイント数は1以上を指定してください。");
            return;
        }

        // 指定された名前の親オブジェクトをシーン内から探す。存在しなければ新しく作成する
        GameObject parent = GameObject.Find(parentName);
        if (parent == null)
        {
            parent = new GameObject(parentName);
            // Undo操作に対応するため、作成をUndo登録
            Undo.RegisterCreatedObjectUndo(parent, "Create Parent Object");
        }

        // 指定された数だけ巡回ポイントを生成し、ランダムに配置する
        for (int i = 0; i < pointCount; i++)
        {
            // 巡回ポイントのGameObjectを新規作成し、Undo登録
            GameObject point = new GameObject($"PatrolPoint_{i}");
            Undo.RegisterCreatedObjectUndo(point, "Create Patrol Point");

            // 親オブジェクトの子に設定
            point.transform.parent = parent.transform;

            // 中心座標と指定された範囲サイズに基づき、X/Y/Zそれぞれランダムな座標を計算
            float randomX = centerPosition.x + Random.Range(-rangeSize.x / 2f, rangeSize.x / 2f);
            float randomY = centerPosition.y + Random.Range(-rangeSize.y / 2f, rangeSize.y / 2f);
            float randomZ = centerPosition.z + Random.Range(-rangeSize.z / 2f, rangeSize.z / 2f);

            // 計算された座標に巡回ポイントを配置
            point.transform.position = new Vector3(randomX, randomY, randomZ);
        }

        // 完了ログを表示
        Debug.Log($"{pointCount}個の巡回ポイントを範囲内にランダム生成しました。");
    }

    /// <summary>
    /// シーンビュー上のイベント（マウスドラッグなど）を処理
    /// </summary>
    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        // Shiftキーが押されているときにのみ、ドラッグによる範囲指定を有効化
        if (e.shift)
        {
            // 左クリックを押した瞬間にドラッグを開始
            if (e.type == EventType.MouseDown && e.button == 0)
            {
                dragStartPos = e.mousePosition;
                dragEndPos = e.mousePosition;
                isDragging = true;
                e.Use(); // イベントを処理済みにして他の処理に渡さない
            }
            // マウスがドラッグされている間は、終点座標を更新して描画を更新
            else if (isDragging && e.type == EventType.MouseDrag && e.button == 0)
            {
                dragEndPos = e.mousePosition;
                sceneView.Repaint(); // 描画を更新（矩形の描画）
                e.Use();
            }
            // ドラッグが終了したら範囲を計算し、ドラッグ状態を解除
            else if (isDragging && (e.type == EventType.MouseUp || e.type == EventType.Ignore) && e.button == 0)
            {
                isDragging = false;
                CalculateBoundsFromDrag(sceneView); // ドラッグ範囲から中心とサイズを計算
                dragStartPos = null;
                e.Use();
            }
        }

        // ドラッグ中で開始位置が設定されていれば、矩形をGUI上に半透明で描画する
        if (isDragging && dragStartPos.HasValue)
        {
            Handles.BeginGUI();
            var rect = GetDragRect(dragStartPos.Value, dragEndPos);
            EditorGUI.DrawRect(rect, new Color(0, 0.5f, 1f, 0.25f)); // 半透明の青色で矩形を描画
            Handles.EndGUI();
        }
    }

    /// <summary>
    /// 2点から矩形Rectを生成（スクリーン座標系）
    /// —始点と終点の座標からドラッグ矩形（Rect）を計算する
    /// </summary>
    private Rect GetDragRect(Vector2 start, Vector2 end)
    {
        return new Rect(
            Mathf.Min(start.x, end.x), // X座標の小さい方を左端に
            Mathf.Min(start.y, end.y), // Y座標の小さい方を上端に
            Mathf.Abs(start.x - end.x), // 幅は差分の絶対値
            Mathf.Abs(start.y - end.y)  // 高さも同様
        );
    }

    /// <summary>
    /// ドラッグされた矩形から範囲の中心位置とサイズを算出
    /// </summary>
    private void CalculateBoundsFromDrag(SceneView sceneView)
    {
        if (!dragStartPos.HasValue)
            return;

        // ドラッグの開始位置と終了位置を、カメラからXZ平面上のワールド座標に変換
        Vector3 worldStart = ScreenToWorldXZ(dragStartPos.Value, sceneView.camera);
        Vector3 worldEnd = ScreenToWorldXZ(dragEndPos, sceneView.camera);

        // 中心座標を開始と終了の中間点に設定
        Vector3 center = (worldStart + worldEnd) * 0.5f;

        // 範囲サイズをXとZの絶対差から計算（Yは0）
        Vector3 size = new Vector3(
            Mathf.Abs(worldEnd.x - worldStart.x),
            0,
            Mathf.Abs(worldEnd.z - worldStart.z)
        );

        centerPosition = center;
        rangeSize = size;

        // 計算結果をログ出力し、インスペクターの再描画を行う
        Debug.Log($"範囲を取得しました。中心: {centerPosition}, サイズ: {rangeSize}");
        Repaint(); // GUI再描画要求
    }

    /// <summary>
    /// スクリーン座標をXZ平面上のワールド座標に変換
    /// </summary>
    private Vector3 ScreenToWorldXZ(Vector2 screenPos, Camera cam)
    {
        // Unityの座標系に合わせるため、Y座標を反転（上端が0のため）
        Vector2 adjustedPos = new Vector2(screenPos.x, cam.pixelHeight - screenPos.y);

        // スクリーン座標からレイを生成
        Ray ray = cam.ScreenPointToRay(adjustedPos);

        // Y軸方向に進まないレイの場合、XZ平面とは交差しないため原点を返す
        if (ray.direction.y == 0)
            return ray.origin;

        // レイがXZ平面（Y=0）と交差する点を求める
        float t = -ray.origin.y / ray.direction.y;
        return ray.origin + ray.direction * t;
    }
}
#endif
