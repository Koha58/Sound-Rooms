using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EC.ONoff == 0)//�����Ȃ��Ƃ�
        {
            Enemys.enabled=false;//���g��\�����\��
                                       // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC.ONoff == 1)//�����Ă���Ƃ�
        {
            Enemys.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
