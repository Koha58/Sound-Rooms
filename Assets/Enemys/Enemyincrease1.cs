using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyincrease1 : MonoBehaviour
{
    public GameObject ebiPrefab;
    public GameObject DestroyPrefab1;
    static public bool isHidden1 = true;
    static public bool Clone1 = false ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHidden1 == false)
        {
            isHidden1 = true;
            GameObject go = Instantiate(ebiPrefab);//�R�s�[�𐶐�
            //Debug.Log(go);
            int px = Random.Range(0, 20);//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
            int pz = Random.Range(0, 20);//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
            go.transform.position = new Vector3(px, 0, pz);
            Clone1 = true;
        }

        if (Clone1 == true)
        {
            Destroy(DestroyPrefab1);
            Clone1 = false;
        }
    }
}
