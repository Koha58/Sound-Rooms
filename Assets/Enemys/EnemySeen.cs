using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySeen : MonoBehaviour
{
    public  float SoundTime;
    [SerializeField] public GameObject EnemyBody;

    //public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody;

    // Start is called before the first frame update
    void Start()
    {
        //SkinnedMeshRendererEnemyBody = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EC.ONoff == 0)//�����Ȃ��Ƃ�
        {
                EnemyBody.SetActive(false);//���g��\�����\��
           // SkinnedMeshRendererEnemyBody.enabled = false;
        }
         if (EC.ONoff == 1)//�����Ă���Ƃ�
         {
                EnemyBody.SetActive(true);//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    } 
}
