using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrop : MonoBehaviour
{ // �쐬����I�u�W�F�N�g��Prefab���w��
    public GameObject objectPrefab;

    // �X�V����
    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown("joystick button 4"))
        {
            SpawnObject();

        }
    }

    // �I�u�W�F�N�g�𐶐����鏈��
    void SpawnObject()
    {
        if (objectPrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0.3f, 0);
            // ���݂̈ʒu�ɃI�u�W�F�N�g�𐶐�
            Instantiate(objectPrefab, spawnPosition, transform.rotation);
        }
        else
        {
            Debug.LogWarning("objectPrefab���ݒ肳��Ă��܂���I");
        }
    }
}
