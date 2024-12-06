using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrop : MonoBehaviour
{ // 作成するオブジェクトのPrefabを指定
    public GameObject objectPrefab;

    // 更新処理
    void Update()
    {
        // スペースキーが押されたら
        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown("joystick button 4"))
        {
            SpawnObject();

        }
    }

    // オブジェクトを生成する処理
    void SpawnObject()
    {
        if (objectPrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0.3f, 0);
            // 現在の位置にオブジェクトを生成
            Instantiate(objectPrefab, spawnPosition, transform.rotation);
        }
        else
        {
            Debug.LogWarning("objectPrefabが設定されていません！");
        }
    }
}
