using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen1 : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj1 = GameObject.FindWithTag("Enemy1");
        EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EC1.ONoff == 0)//�����Ȃ��Ƃ�
        {
            Enemys.enabled = false;//���g��\�����\��
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC1.ONoff == 1)//�����Ă���Ƃ�
        {
            Enemys.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
