using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen8 : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj8 = GameObject.FindWithTag("Enemy8");
        EnemyController8 EC8 = eobj8.GetComponent<EnemyController8>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EC8.ONoff == 0)//�����Ȃ��Ƃ�
        {
            Enemys.enabled = false;//���g��\�����\��
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC8.ONoff == 1)//�����Ă���Ƃ�
        {
            Enemys.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
