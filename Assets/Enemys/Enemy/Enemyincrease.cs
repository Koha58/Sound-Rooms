using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyincrease : MonoBehaviour
{
    [SerializeField]
    private  GameObject ebiPrefab;      //�R�s�[����v���n�u
    [SerializeField]
    private  GameObject DestroyPrefab;  //�j�󂳂��v���n�u
    static  public  bool isHidden = true;      //
    private bool Clone = false;         //Clone�𐶂ݏo������ONOFF
    static  public int enemyDeathcnt = 0;  
    public static float DeathRange = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHidden == false)
        {
            isHidden = true;
            GameObject go = Instantiate(ebiPrefab);//�R�s�[�𐶐�
            //Debug.Log(go);
            int px = Random.Range(0, 20);//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
            int pz = Random.Range(0, 20);//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
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
