using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysGSeen2 : MonoBehaviour
{
    public CapsuleCollider EnemysG;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
        EnemyGController2 EGC2 = eobjG2.GetComponent<EnemyGController2>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EGC2.ONoff == 0)//�����Ȃ��Ƃ�
        {
            EnemysG.enabled = false;//���g��\�����\��
                                    // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EGC2.ONoff == 1)//�����Ă���Ƃ�
        {
            EnemysG.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
