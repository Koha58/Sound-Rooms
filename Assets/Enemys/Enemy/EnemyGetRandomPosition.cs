using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetRandomPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetRandomPosition()
    {
        // �����_����x, y, z���W�𐶐�����
        float randomX = Random.Range(70f, 50f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-95f, -75f);

        // �����������W��Ԃ�
        return new Vector3(randomX, randomY, randomZ);
    }
}
