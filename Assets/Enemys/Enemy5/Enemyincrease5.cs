using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyincrease5 : MonoBehaviour
{
    public GameObject ebiPrefab;      //�R�s�[����v���n�u
    public GameObject DestroyPrefab;  //�j�󂳂��v���n�u
    public bool isHidden = true;      //
    private bool Clone = false;         //Clone�𐶂ݏo������ONOFF
    static public int enemyDeathcnt = 0;  //Enemy�����񂾐�
    public static float DeathRange = 0f;//Enemy�����ʂƍL����͈�

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (isHidden == false)
        {
            isHidden = true;
            GameObject go = Instantiate(ebiPrefab);//�R�s�[�𐶐�
                                                   //Debug.Log(go);
            float px = Random.Range(-140f, -85f); ;//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
            float pz = Random.Range(20f, -20f); ;//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
            go.transform.position = new Vector3(px, 0, pz);

            Clone = true;
        }

        if (Clone == true)
        {
            Destroy(DestroyPrefab);
            Clone = false;
            enemyDeathcnt++;
            DeathRange += 1.0f;
        }

    }
}
