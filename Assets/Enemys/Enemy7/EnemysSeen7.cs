using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen7 : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj7 = GameObject.FindWithTag("Enemy7");
        EnemyController7 EC7 = eobj7.GetComponent<EnemyController7>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EC7.ONoff == 0)//�����Ȃ��Ƃ�
        {
            Enemys.enabled = false;//���g��\�����\��
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC7.ONoff == 1)//�����Ă���Ƃ�
        {
            Enemys.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
