using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen4 : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj4 = GameObject.FindWithTag("Enemy4");
        EnemyController4 EC4 = eobj4.GetComponent<EnemyController4>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EC4.ONoff == 0)//�����Ȃ��Ƃ�
        {
            Enemys.enabled = false;//���g��\�����\��
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC4.ONoff == 1)//�����Ă���Ƃ�
        {
            Enemys.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
