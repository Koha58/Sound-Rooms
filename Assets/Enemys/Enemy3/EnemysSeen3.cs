using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen3 : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj3 = GameObject.FindWithTag("Enemy3");
        EnemyController3 EC3 = eobj3.GetComponent<EnemyController3>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EC3.ONoff == 0)//�����Ȃ��Ƃ�
        {
            Enemys.enabled = false;//���g��\�����\��
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC3.ONoff == 1)//�����Ă���Ƃ�
        {
            Enemys.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
