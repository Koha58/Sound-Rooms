using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen6 : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj6 = GameObject.FindWithTag("Enemy6");
        EnemyController6 EC6 = eobj6.GetComponent<EnemyController6>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EC6 .ONoff == 0)//�����Ȃ��Ƃ�
        {
            Enemys.enabled = false;//���g��\�����\��
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC6.ONoff == 1)//�����Ă���Ƃ�
        {
            Enemys.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
