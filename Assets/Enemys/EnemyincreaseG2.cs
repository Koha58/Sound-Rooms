using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyincreaseG2 : MonoBehaviour
{
    public GameObject ebiPrefab;
    public GameObject DestroyPrefab1;
    static public bool isHiddenG2 = true;
    static public bool CloneG2 = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isHiddenG2 == false)
        {
            isHiddenG2 = true;
            GameObject go = Instantiate(ebiPrefab);//�R�s�[�𐶐�
            //Debug.Log(go);
            int px = Random.Range(0, 20);//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
            int pz = Random.Range(0, 20);//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
            go.transform.position = new Vector3(px, 0, pz);
            CloneG2 = true;
        }

        if (CloneG2 == true)
        {
            Destroy(DestroyPrefab1);
            CloneG2 = false;
            Enemyincrease.enemyDeathcnt++;
        }
    }
}
