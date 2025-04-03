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
        //���x��
        GUILayout.Label("���x��", EditorStyles.boldLabel);
        //�e�L�X�g�t�B�[���h
        myString = EditorGUILayout.TextField("Text Field", myString);

        //ToggleGroup
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //Toggle
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        //Silder
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //ToggleGroup�I��
        EditorGUILayout.EndToggleGroup();

        //�J���[��\��
        EditorGUILayout.ColorField("�J���[", Color.white);
        //�|�b�v�A�b�v��\��
        EditorGUILayout.IntPopup("�|�b�v�A�b�v", 1, new string[] { "��", "��", "��", }, new int[] { 0, 1, 2 });
        //XYZ���W��\��
        EditorGUILayout.Vector3IntField("XYZ���W", Vector3Int.one);
        //�e�L�X�g�t�B�[���h��\��
        EditorGUILayout.TextField("�e�L�X�g�t�B�[���h", "");
        //�w���v�{�b�N�X��\��
        EditorGUILayout.HelpBox("�w���v�{�b�N�X", MessageType.Info);

        //�e�L�X�g�G���A
        text = EditorGUILayout.TextArea(text, GUILayout.Height(100));
        if (GUILayout.Button("�R���\�[���ɏo�́I�I"))
        {
            Debug.Log(text); // �{�^�����N���b�N���ꂽ��A�R���\�[����text���o��
        }
    }

}
