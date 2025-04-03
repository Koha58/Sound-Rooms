using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{
    string myString = "";
    bool groupEnabled;
    bool myBool = false;
    float myFloat = 0.0f;

    string text = "";

    // Add menu named "My Window" to the Window menu
    [MenuItem("Assets/My Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
        window.Show();
    }

    void OnGUI()
    {
        //ラベル
        GUILayout.Label("ラベル", EditorStyles.boldLabel);
        //テキストフィールド
        myString = EditorGUILayout.TextField("Text Field", myString);

        //ToggleGroup
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //Toggle
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        //Silder
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //ToggleGroup終了
        EditorGUILayout.EndToggleGroup();

        //カラーを表示
        EditorGUILayout.ColorField("カラー", Color.white);
        //ポップアップを表示
        EditorGUILayout.IntPopup("ポップアップ", 1, new string[] { "小", "中", "大", }, new int[] { 0, 1, 2 });
        //XYZ座標を表示
        EditorGUILayout.Vector3IntField("XYZ座標", Vector3Int.one);
        //テキストフィールドを表示
        EditorGUILayout.TextField("テキストフィールド", "");
        //ヘルプボックスを表示
        EditorGUILayout.HelpBox("ヘルプボックス", MessageType.Info);

        //テキストエリア
        text = EditorGUILayout.TextArea(text, GUILayout.Height(100));
        if (GUILayout.Button("コンソールに出力！！"))
        {
            Debug.Log(text); // ボタンがクリックされたら、コンソールにtextを出力
        }
    }

}
