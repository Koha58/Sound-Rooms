using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysGSeen : MonoBehaviour
{
    public CapsuleCollider EnemysG;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobjG = GameObject.FindWithTag("EnemyG");
        EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EGC.ONoff == 0)//�����Ȃ��Ƃ�
        {
            EnemysG.enabled = false;//���g��\�����\��
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EGC.ONoff == 1)//�����Ă���Ƃ�
        {
            EnemysG.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }

        GameObject eobjG1 = GameObject.FindWithTag("EnemyG");
        EnemyGController EGC1 = eobjG.GetComponent<EnemyGController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EGC1.ONoff == 0)//�����Ȃ��Ƃ�
        {
            EnemysG.enabled = false;//���g��\�����\��
                                    // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EGC1.ONoff == 1)//�����Ă���Ƃ�
        {
            EnemysG.enabled = true;//���g�\������\��
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
