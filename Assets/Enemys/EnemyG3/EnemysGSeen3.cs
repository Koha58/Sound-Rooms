using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysGSeen3 : MonoBehaviour
{
    public CapsuleCollider EnemysG;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
        EnemyGController3 EGC3 = eobjG3.GetComponent<EnemyGController3>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EGC3.ONoff == 0)//�����Ȃ��Ƃ�
        {
            EnemysG.enabled = false;//���g��\�����\��
                                    // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EGC3.ONoff == 1)//�����Ă���Ƃ�
        {
            EnemysG.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
