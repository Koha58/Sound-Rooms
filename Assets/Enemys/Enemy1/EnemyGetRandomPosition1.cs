using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetRandomPosition1 : MonoBehaviour
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
        float randomX = Random.Range(90f, 70f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-90f, -70f);

        // �����������W��Ԃ�
        return new Vector3(randomX, randomY, randomZ);
    }
}
