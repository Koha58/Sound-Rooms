using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyincrease : MonoBehaviour
{
   
    public  GameObject ebiPrefab;      //�R�s�[����v���n�u
    public  GameObject DestroyPrefab;  //�j�󂳂��v���n�u
    public  bool isHidden = true;      //
    private bool Clone = false;         //Clone�𐶂ݏo������ONOFF
    static  public int enemyDeathcnt = 0;  //Enemy�����񂾐�
    public static float DeathRange = 0f;//Enemy�����ʂƍL����͈�
    private float ParticleTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHidden == false)
        {
            GetComponent<ParticleSystem>().Play();
            ParticleTime += Time.deltaTime;
            if (ParticleTime>1f)
            {
                isHidden = true;
                GameObject go = Instantiate(ebiPrefab);//�R�s�[�𐶐�
                                                       //Debug.Log(go);
                int px = -90;//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
                int pz = 80;//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
                go.transform.position = new Vector3(px, 0, pz);

                Clone = true;
                ParticleTime = 0;
            }
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
