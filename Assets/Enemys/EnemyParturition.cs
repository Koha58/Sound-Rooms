using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParturition : MonoBehaviour
{
    public GameObject ebiPrefab;�@//��������I�u�W�F�N�g������
    static public bool isHidden = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHidden)// false�̎�
        {
            isHidden = true;
            GameObject go = Instantiate(ebiPrefab) as GameObject;
            int px = Random.Range(0, 20);   //X�̂O�`�Q�O�͈̔͐���
            go.transform.position = new Vector3(px, 5, 0);
        }
    }
}
