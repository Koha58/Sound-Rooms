using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen2 : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj2 = GameObject.FindWithTag("Enemy2");
        EnemyController2 EC2 = eobj2.GetComponent<EnemyController2>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EC2.ONoff == 0)//�����Ȃ��Ƃ�
        {
            Enemys.enabled = false;//���g��\�����\��
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC2.ONoff == 1)//�����Ă���Ƃ�
        {
            Enemys.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
