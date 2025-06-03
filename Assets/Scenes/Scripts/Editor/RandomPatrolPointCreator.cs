#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class RandomPatrolPointCreator : EditorWindow
{
    private string parentName = "EnemyPatrolPoints";
    private int pointCount = 10;
    private Vector3 centerPosition = Vector3.zero;
    private Vector3 rangeSize = new Vector3(10, 0, 10);  // XZ平面で範囲指定（Yは0固定でもOK）

    private Vector2? dragStartPos = null;
    private Vector2 dragEndPos;
    private bool isDragging = false;

    [MenuItem("Tools/Random Patrol Point Creator")]
    public static void ShowWindow()
    {
        var window = GetWindow<RandomPatrolPointCreator>("Random Patrol Point Creator");
        SceneView.duringSceneGui += window.OnSceneGUI;
    }

    private void OnDestroy()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnGUI()
    {
        GUILayout.Label("ランダム巡回ポイント生成ツール", EditorStyles.boldLabel);

        parentName = EditorGUILayout.TextField("親オブジェクト名", parentName);
        pointCount = EditorGUILayout.IntField("ポイント数", pointCount);
        centerPosition = EditorGUILayout.Vector3Field("範囲中心位置", centerPosition);
        rangeSize = EditorGUILayout.Vector3Field("範囲サイズ", rangeSize);

        if (GUILayout.Button("ランダム巡回ポイントを生成"))
        {
            CreateRandomPatrolPoints();
        }

        GUILayout.Space(10);
        GUILayout.Label("【Shift + 左クリックドラッグ】でシーンビューに範囲を作成・取得します。", EditorStyles.helpBox);
    }

    private void CreateRandomPatrolPoints()
    {
        if (pointCount <= 0)
        {
            Debug.LogWarning("ポイント数は1以上を指定してください。");
            return;
        }

        GameObject parent = GameObject.Find(parentName);
        if (parent == null)
        {
            parent = new GameObject(parentName);
            Undo.RegisterCreatedObjectUndo(parent, "Create Parent Object");
        }

        for (int i = 0; i < pointCount; i++)
        {
            GameObject point = new GameObject($"PatrolPoint_{i}");
            Undo.RegisterCreatedObjectUndo(point, "Create Patrol Point");
            point.transform.parent = parent.transform;

            float randomX = centerPosition.x + Random.Range(-rangeSize.x / 2f, rangeSize.x / 2f);
            float randomY = centerPosition.y + Random.Range(-rangeSize.y / 2f, rangeSize.y / 2f);
            float randomZ = centerPosition.z + Random.Range(-rangeSize.z / 2f, rangeSize.z / 2f);

            point.transform.position = new Vector3(randomX, randomY, randomZ);
        }

        Debug.Log($"{pointCount}個の巡回ポイントを範囲内にランダム生成しました。");
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        // Shift + 左クリックドラッグで範囲選択
        if (e.shift)
        {
            if (e.type == EventType.MouseDown && e.button == 0)
            {
                dragStartPos = e.mousePosition;
                dragEndPos = e.mousePosition;
                isDragging = true;
                e.Use();
            }
            else if (isDragging && e.type == EventType.MouseDrag && e.button == 0)
            {
                dragEndPos = e.mousePosition;
                sceneView.Repaint();
                e.Use();
            }
            else if (isDragging && (e.type == EventType.MouseUp || e.type == EventType.Ignore) && e.button == 0)
            {
                isDragging = false;
                CalculateBoundsFromDrag(sceneView);
                dragStartPos = null;
                e.Use();
            }
        }

        // ドラッグ矩形の描画
        if (isDragging && dragStartPos.HasValue)
        {
            Handles.BeginGUI();
            var rect = GetDragRect(dragStartPos.Value, dragEndPos);
            EditorGUI.DrawRect(rect, new Color(0, 0.5f, 1f, 0.25f));
            Handles.EndGUI();
        }
    }

    private Rect GetDragRect(Vector2 start, Vector2 end)
    {
        return new Rect(
            Mathf.Min(start.x, end.x),
            Mathf.Min(start.y, end.y),
            Mathf.Abs(start.x - end.x),
            Mathf.Abs(start.y - end.y)
        );
    }

    private void CalculateBoundsFromDrag(SceneView sceneView)
    {
        if (!dragStartPos.HasValue)
            return;

        Vector3 worldStart = ScreenToWorldXZ(dragStartPos.Value, sceneView.camera);
        Vector3 worldEnd = ScreenToWorldXZ(dragEndPos, sceneView.camera);

        Vector3 center = (worldStart + worldEnd) * 0.5f;
        Vector3 size = new Vector3(
            Mathf.Abs(worldEnd.x - worldStart.x),
            0,
            Mathf.Abs(worldEnd.z - worldStart.z)
        );

        centerPosition = center;
        rangeSize = size;

        Debug.Log($"範囲を取得しました。中心: {centerPosition}, サイズ: {rangeSize}");
        Repaint();
    }

    private Vector3 ScreenToWorldXZ(Vector2 screenPos, Camera cam)
    {
        Vector2 adjustedPos = new Vector2(screenPos.x, cam.pixelHeight - screenPos.y);
        Ray ray = cam.ScreenPointToRay(adjustedPos);
        if (ray.direction.y == 0)
            return ray.origin;
        float t = -ray.origin.y / ray.direction.y;
        return ray.origin + ray.direction * t;
    }
}
#endif
