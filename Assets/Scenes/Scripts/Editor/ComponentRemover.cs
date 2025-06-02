using UnityEditor;
using UnityEngine;

public class ComponentRemover : EditorWindow
{
    string componentTypeName = "BoxCollider";

    [MenuItem("Tools/Component Remover")]
    public static void ShowWindow()
    {
        GetWindow<ComponentRemover>("Component Remover");
    }

    void OnGUI()
    {
        GUILayout.Label("Remove Component from All GameObjects", EditorStyles.boldLabel);
        componentTypeName = EditorGUILayout.TextField("Component Type (e.g. BoxCollider)", componentTypeName);

        if (GUILayout.Button("Remove Component"))
        {
            RemoveComponentFromAllObjects(componentTypeName);
        }
    }

    void RemoveComponentFromAllObjects(string typeName)
    {
        var componentType = System.Type.GetType("UnityEngine." + typeName + ", UnityEngine");

        if (componentType == null || !componentType.IsSubclassOf(typeof(Component)))
        {
            Debug.LogError("Invalid component type: " + typeName);
            return;
        }

        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        int removedCount = 0;
        foreach (GameObject obj in allGameObjects)
        {
            Component comp = obj.GetComponent(componentType);
            if (comp != null)
            {
                DestroyImmediate(comp);
                removedCount++;
            }
        }

        Debug.Log($"Removed {removedCount} '{typeName}' components from scene.");
    }
}
