using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGSeen : MonoBehaviour
{
  
    [SerializeField] public GameObject EnemyBody;
    [SerializeField] public GameObject Key;
    [SerializeField] public GameObject Ring;

    //public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody;
    // public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody1; 
    //public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody2;

    // Start is called before the first frame update
    void Start()
    {
        //SkinnedMeshRendererEnemyBody = GetComponent<SkinnedMeshRenderer>();
        //SkinnedMeshRendererEnemyBody1 = GetComponent<SkinnedMeshRenderer>();
        //SkinnedMeshRendererEnemyBody2 = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("EnemyG");
        EnemyGController EGC = eobj.GetComponent<EnemyGController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EGC.ONoff == 0)//�����Ȃ��Ƃ�
        {
            EnemyBody.SetActive(false);//���g��\�����\��
            Key.SetActive(false);//���g��\�����\��
            Ring.SetActive(false);//���g��\�����\��
        }
        if (EGC.ONoff == 1)//�����Ă���Ƃ�
        {
            EnemyBody.SetActive(true);//���g�\������\��
            Key.SetActive(true);//���g�\������\��
            Ring.SetActive(true);//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;
            //SkinnedMeshRendererEnemyBody1.enabled = true;
            //SkinnedMeshRendererEnemyBody2.enabled = true;
        }
    }
}
